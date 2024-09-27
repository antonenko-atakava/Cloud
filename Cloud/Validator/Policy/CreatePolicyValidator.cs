using Cloud.Domain.Http.Request.Policy;
using FluentValidation;

namespace Cloud.Validator.Policy;

public class CreatePolicyValidator : AbstractValidator<CreatePolicyRequest>
{
    public CreatePolicyValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage("Название политики не должно быть пустым")
            .Length(3, 100)
            .WithMessage("Название политики не должно быть больше 100 символов или меньше 3");
    }
}