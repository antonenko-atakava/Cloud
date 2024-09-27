using Cloud.Domain.Http.Request.Policy;
using FluentValidation;

namespace Cloud.Validator.Policy;

public class GetByNamePolicyValidator : AbstractValidator<GetByNamePolicyRequest>
{
    public GetByNamePolicyValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage("Название политики не должно быть пустым")
            .Length(3, 100)
            .WithMessage("Название политики не должно быть больше 100 символов или меньше 3");
    }
}