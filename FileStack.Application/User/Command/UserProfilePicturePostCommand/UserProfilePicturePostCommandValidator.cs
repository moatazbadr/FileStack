using FluentValidation;

namespace FileStack.Application.User.Command.UserProfilePicturePostCommand;

public class UserProfilePicturePostCommandValidator :  AbstractValidator<UserProfilePicturePostCommand>
{
    public UserProfilePicturePostCommandValidator()
    {
        RuleFor(x => x.ProfilePicture)
             .NotNull()
             .Must(x=>x.Length >0)
             .WithMessage("Profile picture is required.");
        RuleFor(x=>x.ProfilePicture!.ContentType)
            .Must(contentType => contentType == "image/jpeg" || contentType == "image/png" || contentType == "image/gif")
            .WithMessage("Only JPEG, PNG, and GIF image formats are allowed.");

        
        RuleFor(x => x.ProfilePicture!.Length)
            .LessThanOrEqualTo(10 * 1024 * 1024) 
            .WithMessage("Profile picture size must be less than or equal to 10 MB.");


    }
}
