using FileStack.Application.APIResponses;
using FileStack.Application.Interfaces;
using MediatR;

namespace FileStack.Application.User.Command.UserEmailConfirmation;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, OTPVerficiactionResponse>
{
    private readonly IauthService _authService;

    public ConfirmEmailCommandHandler( IauthService iauthService)
    {
        _authService = iauthService;
        
    }

    public async Task<OTPVerficiactionResponse> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
       
        var response = await _authService.Verify(request.Email, request.OtpCode);

        return response;

    }
}
