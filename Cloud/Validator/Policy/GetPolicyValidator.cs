using Cloud.Domain.Http.Request.Policy;
using FluentValidation;

namespace Cloud.Validator.Policy;

public class GetPolicyValidator : AbstractValidator<GetPolicyRequest>
{
    public GetPolicyValidator()
    {
        RuleFor(f => f.Id)
            .NotEmpty()
            .WithMessage("ID не должен быть пустым");
    }
}