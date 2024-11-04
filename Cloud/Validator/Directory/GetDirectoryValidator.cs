using Cloud.Domain.Http.Request.Directory;
using FluentValidation;

namespace Cloud.Validator.Directory;

public class GetDirectoryValidator : AbstractValidator<GetDirectoryRequest>
{
    public GetDirectoryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Идентификатор директории не может быть пустым.");
    }
}