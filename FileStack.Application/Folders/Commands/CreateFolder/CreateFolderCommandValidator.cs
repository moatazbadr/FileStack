using FluentValidation;

namespace FileStack.Application.Folders.Commands.CreateFolder;

public class CreateFolderCommandValidator : AbstractValidator<CreateFolderCommand>

{
    public CreateFolderCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Folder name is required.")
            .MaximumLength(150).WithMessage("Folder name must not exceed 150 characters.");
        //RuleFor(x => x.ParentFolderId)
        //    .GreaterThan(0).When(x => x.ParentFolderId.HasValue)
        //    .WithMessage("Incorrect folder Id");


    }
}
