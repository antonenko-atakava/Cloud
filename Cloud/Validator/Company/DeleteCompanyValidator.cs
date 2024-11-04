using Cloud.Domain.Http.Request.Company;
using FluentValidation;

namespace Cloud.Validator.Company;

public class DeleteCompanyValidator : AbstractValidator<DeleteCompanyRequest>
{
    public DeleteCompanyValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ID не должен быть пустым");
    }
}