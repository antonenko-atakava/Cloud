using Cloud.Domain.Http.Request.Policy;
using FluentValidation;

namespace Cloud.Validator.Policy;

public class DeletePolicyValidator : AbstractValidator<DeletePolicyRequest>
{
    public DeletePolicyValidator()
    {
        RuleFor(f => f.Name)
            .NotEmpty()
            .WithMessage("название не должно быть пустым");
    }
}