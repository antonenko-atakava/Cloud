using Cloud.Domain.Http.Request.Directory;
using FluentValidation;

namespace Cloud.Validator.Directory;

public class UpdatePathDirectoryValidator : AbstractValidator<UpdatePathDirectoryRequest>
{
    public UpdatePathDirectoryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Идентификатор директории не может быть пустым.");

        RuleFor(x => x.NewPath)
            .NotEmpty().WithMessage("Новый путь не может быть пустым.")
            .Must(BeAValidPath).WithMessage("Неверный формат нового пути.");
    }

    private bool BeAValidPath(string path)
    {
        return !string.IsNullOrEmpty(path);
    }
}