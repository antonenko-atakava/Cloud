using Cloud.Domain.Http.Request.Company;
using FluentValidation;

namespace Cloud.Validator.Company;

public class UpdateCompanyValidator : AbstractValidator<UpdateCompanyRequest>
{
    public UpdateCompanyValidator()
    {
        
    }
}