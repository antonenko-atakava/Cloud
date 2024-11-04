using Cloud.Domain.Http.Request.Directory;
using FluentValidation;

namespace Cloud.Validator.Directory;

public class GetByNameDirectoryRequestValidator : AbstractValidator<GetByNameDirectoryRequest>
{
    public GetByNameDirectoryRequestValidator()
    {
        RuleFor(x => x.NameDirectory)
            .NotEmpty()
            .WithMessage("NameDirectory не должен быть пустым.");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId не должен быть пустым.");
    }
}