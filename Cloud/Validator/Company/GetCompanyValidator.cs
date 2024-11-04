using Cloud.Domain.Http.Request.Company;
using FluentValidation;

namespace Cloud.Validator.Company;

public class GetCompanyValidator : AbstractValidator<GetCompanyRequest>
{
    public GetCompanyValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ID не должен быть пустым");
    }
}