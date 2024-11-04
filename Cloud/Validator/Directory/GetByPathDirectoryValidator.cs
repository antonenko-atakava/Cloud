using Cloud.Domain.Http.Request.Directory;
using FluentValidation;

namespace Cloud.Validator.Directory;

public class GetByPathDirectoryValidator : AbstractValidator<GetByPathDirectoryRequest>
{
    public GetByPathDirectoryValidator()
    {
        RuleFor(x => x.Path)
            .NotEmpty().WithMessage("Путь не может быть пустым.")
            .Must(BeAValidPath).WithMessage("Неверный формат пути.");
    }

    private bool BeAValidPath(string path)
    {
        return !string.IsNullOrEmpty(path);
    }
}