using FluentValidation;

namespace FileStack.Application.Folders.Queries;

public class GetFolderQueryValidator :AbstractValidator<GetFolderQuery>
{
    public GetFolderQueryValidator()
    {
        RuleFor(x => x.FolderName)
            .NotEmpty().WithMessage("Folder name must not be empty.")
            .MaximumLength(255).WithMessage("Folder name must not exceed 255 characters.");




    }
}
