namespace FileStack.Infrastructure.Repositories;
public class AuthService (
UserManager<ApplicationUser> _userManager,
RoleManager<IdentityRole> _roleManager,
ItokenHandler _itokenHandler,
IMailingService _mailing,
ApplicationDbContext _context): IauthService
{
 



    public async Task<OTPToken> GetOTPToken(string Email,string otpCode)
    {
        
        var hashedOtp = BCrypt.Net.BCrypt.HashPassword(otpCode);
        var previousOtps = await _context.OTPTokens.Where(otp => otp.UserEmail == Email && ! otp.IsUsed).ToListAsync();
        foreach (var oldOtp in previousOtps)
        {
            oldOtp.IsUsed = true; 
        }
        var otpEntity = new OTPToken
        {
            AttemptCount = 0,
            CodeHash = hashedOtp,
            CreatedAt = DateTime.UtcNow,
            ExpirationTime = DateTime.UtcNow.AddMinutes(10),
            IsUsed = false,
            PurposeOfOTP = PurposeOTP.AccountVerification,
            UserEmail = Email
        };
        return otpEntity;
    }

    #region Login Function
    public async Task<LoginResponse> LoginAsync(LoginRequestDTO loginRequest)
    {
        var existingUser = await _userManager.FindByEmailAsync(loginRequest.Email);
        if (existingUser == null)
        {
            return new LoginResponse
            {
                Message = "Email or password is invalid"
            };
        }
        var checkPassword = await _userManager.CheckPasswordAsync(existingUser, loginRequest.Password);
        if (!checkPassword)
        {
            return new LoginResponse
            {
                Message = "Email or password is invalid"
            };

        }
        var UserRoles = (await _userManager.GetRolesAsync(existingUser)).ToList();
        var UserToken = await _itokenHandler.CreateJwtToken(existingUser);

        var loginResponse = new LoginResponse()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(UserToken),
            Roles = UserRoles.ToList(),
            ExpiresAt = UserToken.ValidTo,
            Message = "Login successful"

        };

        return loginResponse;
    } 
    #endregion

    //IMPORTANT: Refactor this method to implement Single Responsibility Principle (SRP)
    public async Task<RegisterResponse> RegisterUserAsync(RegisterRequestDTO registerRequest)
    {
        // Check if email already exists
        var existingUser = await _userManager.FindByEmailAsync(registerRequest.Email);
        if (existingUser is not null)
        {
            return new RegisterResponse
            {
                Message = "If this Email is valid, an OTP has been sent to this Email",
                Success = true
            };
        }


        var Tempuser = await _context.TempUsers.FirstOrDefaultAsync(u => u.Email == registerRequest.Email);
        if (Tempuser == null)
        {

            Tempuser = new TempUser()
            {
                Email = registerRequest.Email,
            };
            await _context.TempUsers.AddAsync(Tempuser);

        }
        Tempuser.PasswordPlain= registerRequest.Password;
        Tempuser.FirstName = registerRequest.FirstName;
        Tempuser.LastName = registerRequest.LastName;
        Tempuser.BirthDate = registerRequest.BirthDate;
        
        await _context.SaveChangesAsync();
        var OtpEntity = new OTPToken();
        var OtpCode = RandomNumberGenerator.GetInt32(100000, 999999).ToString();

        using var transcation = await _context.Database.BeginTransactionAsync();
        try
        {
           OtpEntity= await GetOTPToken(registerRequest.Email, OtpCode);
            await _context.OTPTokens.AddAsync(OtpEntity);
            await _context.SaveChangesAsync();

            await transcation.CommitAsync();
        }
        catch (Exception ex)
        {
            await transcation.RollbackAsync();
            return new RegisterResponse
            {
                Message = "Registration failed. Please try again later.",
                Success = false
            };
        }
       


        await sendOtpMail(registerRequest.Email,OtpCode, registerRequest.FirstName);
        return new RegisterResponse
        {
            Message = "an OTP has been sent to this Email",
            Success = true
        };
    }
    

    private async Task sendOtpMail(string email, string otpCode, string firstName)
    {
        var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Constants", "Email.html");
        var emailTemplate = await System.IO.File.ReadAllTextAsync(templatePath);
        var emailBody = emailTemplate.Replace("{{OTP}}", otpCode)
                                     .Replace("{{FirstName}}", firstName);
        await _mailing.SendEmailAsync(email, "Verify your Email", emailBody);
    }

    public async Task<OTPVerficiactionResponse> Verify(string Email, string OtpCode)
    {
        var TempUser = await _context.TempUsers.FirstOrDefaultAsync(u => u.Email == Email);
        var otpResponse = new OTPVerficiactionResponse();
        if (TempUser is null)
        {
            otpResponse.Message = "Invalid OTP or Email.";
            otpResponse.IsVerified = false;
            return otpResponse;
        }
        if (TempUser.IsVerified)
        {
            otpResponse.Message = "User is already verified.";
            otpResponse.IsVerified = true;
            return otpResponse;
        }
        var otpEntity = await _context.OTPTokens
            .Where(otp => otp.UserEmail == Email && !otp.IsUsed && otp.PurposeOfOTP == PurposeOTP.AccountVerification)
            .OrderByDescending(otp => otp.CreatedAt)
            .FirstOrDefaultAsync();
        if (otpEntity is null)
        {
            otpResponse.Message = "Invalid OTP or Email.";
            otpResponse.IsVerified = false;
            return otpResponse;
        }
     if ( otpEntity.ExpirationTime < DateTime.UtcNow )
        {
            otpResponse.Message = "Invalid OTP or Email.";
            otpResponse.IsVerified = false;
            otpEntity.IsUsed = true;
            return otpResponse;
        }
        if (otpEntity.AttemptCount >= 5)
        {
            otpResponse.Message = "Maximum OTP verification attempts exceeded.";
            otpResponse.IsVerified = false;
            return otpResponse;
        }
        if (!BCrypt.Net.BCrypt.Verify(OtpCode, otpEntity.CodeHash))
        {
            otpEntity.AttemptCount += 1;
            await _context.SaveChangesAsync();
            otpResponse.Message = "Invalid OTP or Email.";
            otpResponse.IsVerified = false;
            return otpResponse;

        }

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            otpEntity.IsUsed = true;
            TempUser.IsVerified = true;

            var identityResult = await CreateIdentityUser(TempUser);
            if (!identityResult.Succeeded)
                throw new Exception("Identity user creation failed.");

            await transaction.CommitAsync();

            otpResponse.IsVerified = true;
            otpResponse.Message = "OTP verified successfully. User account created.";
            return otpResponse;
        }
        catch
        {
            await transaction.RollbackAsync();
            return new OTPVerficiactionResponse { IsVerified = false, Message = "Verification failed. Please try again." };
        }

    }
    private async Task <IdentityResult> CreateIdentityUser(TempUser tempUser)
    {
        var IdentityUser = new ApplicationUser()
        {
            Email = tempUser.Email,
            FirstName = tempUser.FirstName,
            LastName = tempUser.LastName,   
            BirthDate = tempUser.BirthDate,
            UserName= tempUser.Email,
            EmailConfirmed =true
          
        };

        var result=await _userManager.CreateAsync(IdentityUser,tempUser.PasswordPlain);

        if (result.Succeeded) {
        await _userManager.AddToRoleAsync(IdentityUser,ValidUserRoles.User);
            _context.Remove(tempUser);
            await _context.SaveChangesAsync();
        }
     
         return result;


    }

  
}
