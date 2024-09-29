using Cloud.Domain.Http.Request.User;
using FluentValidation;

namespace Cloud.Validator.User;

public class LoginUserValidator : AbstractValidator<LoginUserRequest>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty()
            .WithMessage("Логин не должен быть пустым")
            .MinimumLength(50)
            .WithMessage("Логин не может быть больше 50 символов");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Пароль не может быть пустым.")
            .MinimumLength(6).WithMessage("Пароль должен содержать не менее 6 символов.");
    }
}