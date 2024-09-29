using Cloud.Domain.Http.Request.Role;
using FluentValidation;

namespace Cloud.Validator.Role;

public class DeleteRoleValidator : AbstractValidator<DeleteRoleRequest>
{
    public DeleteRoleValidator()
    {
        RuleFor(f => f.Id)
            .NotEmpty()
            .WithMessage("ID не должен быть пустым");
    }
}