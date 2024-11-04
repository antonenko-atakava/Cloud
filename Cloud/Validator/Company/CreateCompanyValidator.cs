using Cloud.Domain.Http.Request.Company;
using FluentValidation;

namespace Cloud.Validator.Company;

public class CreateCompanyValidator : AbstractValidator<CreateCompanyRequest>
{
    public CreateCompanyValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage("Название Компании не должно быть пустым")
            .Length(3, 150)
            .WithMessage("Название Компании не должно быть больше 150 символов или меньше 3");
    }
}