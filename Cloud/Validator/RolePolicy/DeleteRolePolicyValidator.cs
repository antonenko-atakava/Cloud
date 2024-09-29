using Cloud.Domain.Http.Request.RolePolicy;
using FluentValidation;

namespace Cloud.Validator.RolePolicy;

public class DeleteRolePolicyValidator : AbstractValidator<DeleteRolePolicyRequest>
{
    public DeleteRolePolicyValidator()
    {
        RuleFor(x => x.PolicyId)
            .NotEmpty().WithMessage("Policy Id это обязательное поле.")
            .NotEqual(Guid.Empty).WithMessage("Policy Id должен быть полным.");

        RuleFor(x => x.RoleId)
            .NotEmpty().WithMessage("Role Id это обязательное поле.")
            .NotEqual(Guid.Empty).WithMessage("Role Id должен быть полным.");
    }
}