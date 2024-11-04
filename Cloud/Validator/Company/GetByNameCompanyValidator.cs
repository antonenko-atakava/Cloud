using Cloud.Domain.Http.Request.Company;
using FluentValidation;

namespace Cloud.Validator.Company;

public class GetByNameCompanyValidator : AbstractValidator<GetByNameCompanyRequest>
{
    public GetByNameCompanyValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage("Название политики не должно быть пустым")
            .Length(3, 150)
            .WithMessage("Название компании не должно быть больше 150 символов или меньше 3");
    }
}