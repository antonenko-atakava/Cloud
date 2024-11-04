using Cloud.Domain.Http.Request.Directory;
using FluentValidation;

namespace Cloud.Validator.Directory;

public class GetSubDirectoriesRequestValidator : AbstractValidator<GetSubDirectoriesRequest>
{
    public GetSubDirectoriesRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id не должен быть пустым.");
    }
}