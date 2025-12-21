using FileStack.Application.APIResponses;
using FileStack.Application.DTOS;
using FileStack.Application.Interfaces;
using FileStack.Domain.Entities;
using FileStack.Infrastructure.Constants;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace FileStack.Infrastructure.Repositories
{
    public class AuthService : IauthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ItokenHandler _itokenHandler;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ItokenHandler itokenHandler)
        {
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
                    Message = "User with this email already exists",
                    Success = false
                };
            }

            // Map DTO to entity
            var newUser = new ApplicationUser
            {
                UserName = registerRequest.Email,
                Email = registerRequest.Email,
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                BirthDate = registerRequest.BirthDate
            };

            // Create user
            var createUserResult = await _userManager.CreateAsync(newUser, registerRequest.Password);
            if (!createUserResult.Succeeded)
            {
                var errors = string.Join(", ", createUserResult.Errors.Select(e => e.Description));
                return new RegisterResponse
                {
                    Message = $"User registration failed: {errors}",
                    Success = false
                };
            }

            // Check if the required role exists
            var requiredRole = await _roleManager.FindByNameAsync(ValidUserRoles.User);

            if (requiredRole is null)
            {
                return new RegisterResponse
                {
                    Message = $"Role '{ValidUserRoles.User}' does not exist in the database",
                    Success = false
                };
            }

            // Assign role to user
            var roleAssignResult = await _userManager.AddToRoleAsync(newUser, requiredRole.Name);

            if (!roleAssignResult.Succeeded)
            {
                var errors = string.Join(", ", roleAssignResult.Errors.Select(e => e.Description));
                return new RegisterResponse
                {
                    Message = $"User created but failed to assign role: {errors}",
                    Success = false
                };
            }

            // SUCCESS
            return new RegisterResponse
            {
                Message = "User registered successfully",
                Success = true
            };
        }

        public Task<OTPVerficiactionResponse> Verify(string Email, string OtpCode)
        {
        

            throw new NotImplementedException();
        }
    }
}
