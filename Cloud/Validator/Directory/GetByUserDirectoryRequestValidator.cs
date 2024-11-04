using Cloud.Domain.Http.Request.Directory;
using FluentValidation;

namespace Cloud.Validator.Directory;

public class GetByUserDirectoryRequestValidator : AbstractValidator<GetByUserDirectoryRequest>
{
    public GetByUserDirectoryRequestValidator()
    {
        RuleFor(x => x.Path)
            .NotEmpty()
            .WithMessage("Path не должен быть пустым.");
        
        RuleFor(x => x.DirectoryName)
            .NotEmpty()
            .WithMessage("DirectoryName не должен быть пустым.");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId не должен быть пустым.");
    }
}