using Cloud.Domain.Http.Request.Directory;
using FluentValidation;

namespace Cloud.Validator.Directory;

public class UpdateIconDirectoryValidator : AbstractValidator<UpdateIconDirectoryRequest>
{
    public UpdateIconDirectoryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Идентификатор директории не может быть пустым.");

        RuleFor(x => x.Icon)
            .NotNull().WithMessage("Иконка не может быть пустой.");
    }
}