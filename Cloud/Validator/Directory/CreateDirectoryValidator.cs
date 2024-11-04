using Cloud.Domain.Http.Request.Directory;
using FluentValidation;

namespace Cloud.Validator.Directory;

public class CreateDirectoryValidator : AbstractValidator<CreateDirectoryRequest>
{
    public CreateDirectoryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("Идентификатор пользователя не может быть пустым.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Имя директории не может быть пустым.")
            .MaximumLength(255).WithMessage("Имя директории не должно превышать 255 символов.");

        RuleFor(x => x.Path)
            .NotEmpty().WithMessage("Путь не может быть пустым.")
            .Must(BeAValidPath).WithMessage("Неверный формат пути.");
    }

    private bool BeAValidPath(string path)
    {
        return !string.IsNullOrEmpty(path);
    }
}