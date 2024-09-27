using Cloud.Domain.Http.Request.User;
using FluentValidation;

namespace Cloud.Validator.User;

public class GetUserValidator : AbstractValidator<GetUserRequest>
{
    public GetUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id это обязательное поле.")
            .NotEqual(Guid.Empty).WithMessage("Id должен быть полным.");
    }
}