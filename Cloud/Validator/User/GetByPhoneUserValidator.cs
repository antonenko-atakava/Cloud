using Cloud.Domain.Http.Request.User;
using FluentValidation;

namespace Cloud.Validator.User;

public class GetByPhoneUserValidator : AbstractValidator<GetByPhoneUserRequest>
{
    public GetByPhoneUserValidator()
    {
        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Номер телефона не должен быть пустым")
            .Matches(@"^\+?\d{7,15}$").WithMessage("Номер телефона должен быть от 7 до 15 символов и начинатся с '+'");;
    }
}