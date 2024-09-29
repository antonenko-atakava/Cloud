using Cloud.Domain.Http.Request.Role;
using Cloud.Domain.Http.Request.RolePolicy;
using FluentValidation;

namespace Cloud.Validator.Role;

public class CreateRoleValidator : AbstractValidator<CreateRoleRequest>
{
    public CreateRoleValidator()
    {
        RuleFor(f => f.Name)
            .NotEmpty()
            .WithMessage("Название не должен быть пустым")
            .MaximumLength(100)
            .WithMessage("Название роли не должно быть больше 100 символов");
    }
}