using Cloud.Domain.Http.Request.UserRole;
using FluentValidation;

namespace Cloud.Validator.UserRole;

public class CreateUserRoleValidator : AbstractValidator<CreateUserRoleRequest>
{
    public CreateUserRoleValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User Id это обязательное поле.")
            .NotEqual(Guid.Empty).WithMessage("User Id должен быть полным.");

        RuleFor(x => x.RoleId)
            .NotEmpty().WithMessage("Role Id это обязательное поле.")
            .NotEqual(Guid.Empty).WithMessage("Role Id должен быть полным.");
    }
}