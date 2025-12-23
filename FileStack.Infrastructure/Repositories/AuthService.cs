using FileStack.Application.APIResponses;
using FileStack.Application.DTOS;
using FileStack.Application.Interfaces;
using FileStack.Domain.Constants;
using FileStack.Domain.Entities;
using FileStack.Infrastructure.Constants;
using FileStack.Infrastructure.MailService;
using FileStack.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using static System.Net.WebRequestMethods;

namespace FileStack.Infrastructure.Repositories
{
    public class AuthService : IauthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ItokenHandler _itokenHandler;
        private readonly IMailingService _mailing;
        private  readonly ApplicationDbContext _context;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ItokenHandler itokenHandler,
            IMailingService mailing,
            ApplicationDbContext context

            )
        {
            _context = context;
            _mailing = mailing;
            _userManager = userManager;
            _roleManager = roleManager;
            _itokenHandler = itokenHandler;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequestDTO loginRequest)
        {
            var existingUser =await _userManager.FindByEmailAsync(loginRequest.Email);
            if (existingUser == null) {
                return new LoginResponse
                {
                    Message="Email or password is invalid"
                };
            }
            var checkPassword = await _userManager.CheckPasswordAsync(existingUser , loginRequest.Password);
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

            
            var Tempuser = _context.TempUsers.FirstOrDefault(u => u.Email == registerRequest.Email);
            if (Tempuser == null) {

                Tempuser = new TempUser()
                {
                    Email = registerRequest.Email,
                };
                await _context.TempUsers.AddAsync(Tempuser);

            }
            var Hasher = new PasswordHasher<TempUser>();
            var hashedPassword = Hasher.HashPassword(Tempuser, registerRequest.Password);
            Tempuser.FirstName = registerRequest.FirstName;
            Tempuser.LastName = registerRequest.LastName;
            Tempuser.BirthDate = registerRequest.BirthDate;
            Tempuser.PasswordHash = hashedPassword;

            var recentOtps = await _context.OTPTokens
    .Where(x => x.UserEmail == registerRequest.Email &&
                x.CreatedAt > DateTime.UtcNow.AddMinutes(-1))
    .CountAsync();

            if (recentOtps > 0)
            {
                return new RegisterResponse
                {
                    Success = true,
                    Message = "If this email is valid, an OTP has been sent."
                };
            }
            var otpCode =  RandomNumberGenerator.GetInt32(100000,999999).ToString(); // Generate a 6-digit OTP code
            var hashedOtp = BCrypt.Net.BCrypt.HashPassword(otpCode);
            var OtpEntity = new OTPToken();
            var previousOtps = await _context.OTPTokens
    .Where(t => t.UserEmail == registerRequest.Email && !t.IsUsed).ToListAsync();

            foreach (var oldOtp in previousOtps)
            {
                oldOtp.IsUsed = true; // Mark as used/invalidated so only the NEW one works
            }
            using var transcation = await _context.Database.BeginTransactionAsync();
            try
            {
                OtpEntity = new OTPToken
                {
                    AttemptCount = 0,
                    CodeHash = hashedOtp,
                    CreatedAt = DateTime.UtcNow,
                    ExpirationTime = DateTime.UtcNow.AddMinutes(10),
                    IsUsed = false,
                    PurposeOfOTP = PurposeOTP.AccountVerification,
                    UserEmail = registerRequest.Email
                };
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
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Constants", "Email.html");
            var emailTemplate = await System.IO.File.ReadAllTextAsync(templatePath);
            var emailBody = emailTemplate.Replace("{{OTP}}", otpCode)
                                         .Replace("{{FirstName}}", registerRequest.FirstName);

          await   _mailing.SendEmailAsync(registerRequest.Email, "Verify your Email", emailBody);  

            return new RegisterResponse
            {
                Message = "an OTP has been sent to this Email",
                Success = true
            };




        }
            #region Old Registeration
            //var newUser = new ApplicationUser
            //{
            //    UserName = registerRequest.Email,
            //    Email = registerRequest.Email,
            //    FirstName = registerRequest.FirstName,
            //    LastName = registerRequest.LastName,
            //    BirthDate = registerRequest.BirthDate
            //};

            //// Create user
            //var createUserResult = await _userManager.CreateAsync(newUser, registerRequest.Password);
            //if (!createUserResult.Succeeded)
            //{
            //    var errors = string.Join(", ", createUserResult.Errors.Select(e => e.Description));
            //    return new RegisterResponse
            //    {
            //        Message = $"User registration failed: {errors}",
            //        Success = false
            //    };
            //}

            //// Check if the required role exists
            //var requiredRole = await _roleManager.FindByNameAsync(ValidUserRoles.User);

            //if (requiredRole is null)
            //{
            //    return new RegisterResponse
            //    {
            //        Message = $"Role '{ValidUserRoles.User}' does not exist in the database",
            //        Success = false
            //    };
            //}

            //// Assign role to user
            //var roleAssignResult = await _userManager.AddToRoleAsync(newUser, requiredRole.Name);

            //if (!roleAssignResult.Succeeded)
            //{
            //    var errors = string.Join(", ", roleAssignResult.Errors.Select(e => e.Description));
            //    return new RegisterResponse
            //    {
            //        Message = $"User created but failed to assign role: {errors}",
            //        Success = false
            //    };
            //}

            //// SUCCESS
            //return new RegisterResponse
            //{
            //    Message = "User registered successfully",
            //    Success = true
            //}; 
            #endregion

        public Task<OTPVerficiactionResponse> Verify(string Email, string OtpCode)
        {
        
            throw new NotImplementedException();
        }
    }
}
