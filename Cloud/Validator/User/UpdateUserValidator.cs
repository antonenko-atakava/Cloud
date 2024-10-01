using Cloud.Domain.Http.Request.User;
using FluentValidation;

namespace Cloud.Validator.User;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Идентификатор пользователя не должен быть пустым.");

        RuleFor(x => x.Login)
            .NotEmpty().When(x => x.Login != null).WithMessage("Логин не должен быть пустым.")
            .Length(3, 50).When(x => x.Login != null).WithMessage("Логин должен содержать от 3 до 50 символов.");

        RuleFor(x => x.Email)
            .EmailAddress().When(x => x.Email != null).WithMessage("Неверный формат электронной почты.");

        RuleFor(x => x.Phone)
            .Matches(@"^\+?\d{7,15}$").When(x => x.Phone != null).WithMessage("Номер телефона должен содержать от 7 до 15 цифр и может начинаться с '+'.");
    }
}