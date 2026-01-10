using FluentValidation;

namespace FileStack.Application.Folders.Commands.RenameFolder;

public class RenameFolderCommandValidator :AbstractValidator<RenameFolderCommand>
{
    public RenameFolderCommandValidator()
    {
        RuleFor(x=>x.FolderId)
            .Must(id => id > 0)
            .WithMessage("FolderId must be greater than zero.");

        RuleFor(x=>x.NewName)
            .NotEmpty()
            .WithMessage("NewName cannot be empty.")
            .MaximumLength(255)
            .WithMessage("NewName cannot exceed 255 characters.");

    }
}
