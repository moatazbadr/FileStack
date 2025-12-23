using FileStack.Application.APIResponses;
using FileStack.Application.DTOS;

namespace FileStack.Application.Interfaces
{
    public interface IauthService
    {
      Task<RegisterResponse> RegisterUserAsync(RegisterRequestDTO registerRequest);
      Task<LoginResponse> LoginAsync(LoginRequestDTO loginRequest);
      Task<OTPVerficiactionResponse> Verify(string Email, string OtpCode);


    }
}
