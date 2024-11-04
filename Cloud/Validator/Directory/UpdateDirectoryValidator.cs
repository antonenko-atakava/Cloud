using Cloud.Domain.Http.Request.Directory;
using FluentValidation;

namespace Cloud.Validator.Directory;

public class UpdateDirectoryValidator : AbstractValidator<UpdateDirectoryRequest>
{
    public UpdateDirectoryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Идентификатор директории не может быть пустым.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Имя директории не может быть пустым.")
            .MaximumLength(255).WithMessage("Имя директории не должно превышать 255 символов.");
    }
}