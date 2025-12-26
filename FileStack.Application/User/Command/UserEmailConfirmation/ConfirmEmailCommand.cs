using FileStack.Application.APIResponses;
using MediatR;

namespace FileStack.Application.User.Command.UserEmailConfirmation;

public class ConfirmEmailCommand :IRequest<OTPVerficiactionResponse>
{
    public string Email { get; set; } = string.Empty;
    public string OtpCode { get; set; } = string.Empty;

}
