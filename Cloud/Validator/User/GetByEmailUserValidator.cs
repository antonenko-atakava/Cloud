using Cloud.Domain.Http.Request.User;
using FluentValidation;

namespace Cloud.Validator.User;

public class GetByEmailUserValidator : AbstractValidator<GetByEmailUserRequest>
{
    public GetByEmailUserValidator()
    {
        RuleFor(request => request.Email)
            .NotEmpty().WithMessage("Электронная почта не может быть пустой.")
            .EmailAddress().WithMessage("Недопустимый формат электронной почты.");
    }
}