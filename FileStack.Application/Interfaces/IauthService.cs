using FileStack.Application.APIResponses;
using FileStack.Application.DTOS;
using FileStack.Domain.Entities;

namespace FileStack.Application.Interfaces
{
    public interface IauthService
    {
      Task<RegisterResponse> RegisterUserAsync(RegisterRequestDTO registerRequest);
      Task<LoginResponse> LoginAsync(LoginRequestDTO loginRequest);
      Task<OTPVerficiactionResponse> Verify(string Email, string OtpCode);
      Task<OTPToken> GetOTPToken(string Email,string otpCode);



    }
}
