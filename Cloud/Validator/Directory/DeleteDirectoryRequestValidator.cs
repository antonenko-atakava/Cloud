using Cloud.Domain.Http.Request.Directory;
using FluentValidation;

namespace Cloud.Validator.Directory;

public class DeleteDirectoryRequestValidator : AbstractValidator<DeleteDirectoryRequest>
{
    public DeleteDirectoryRequestValidator()
    {
        RuleFor(request => request.Id)
            .NotEmpty()
            .WithMessage("Id не должен быть пустым.");
    }
}