using FluentValidation;

namespace FileStack.Application.User.Command.UserEmailConfirmation;

public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>

{
    public ConfirmEmailCommandValidator()
    {
        RuleFor(x => x.OtpCode).NotEmpty().Length(6).WithMessage("Otp must be 6 digits");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Enter a valid Email address");

        
    }
}
