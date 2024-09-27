using Cloud.Domain.Http.Request.UserPolicy;
using FluentValidation;

namespace Cloud.Validator.UserPolicy;

public class CreateUserPolicyValidator : AbstractValidator<CreateUserPolicyRequest>
{
    public CreateUserPolicyValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId не может быть пустым.");

        RuleFor(x => x.PolicyId)
            .NotEmpty().WithMessage("PolicyId не может быть пустым.");
    }
}