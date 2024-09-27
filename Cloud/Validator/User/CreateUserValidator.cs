using Cloud.Domain.Http.Request.User;
using FluentValidation;

namespace Cloud.Validator.User;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(request => request.Login)
            .NotEmpty().WithMessage("Логин не может быть пустым.")
            .MinimumLength(3).WithMessage("Логин должен содержать не менее 3 символов.")
            .MaximumLength(50).WithMessage("Логин не может превышать 50 символов.");

        RuleFor(request => request.Password)
            .NotEmpty().WithMessage("Пароль не может быть пустым.")
            .MinimumLength(6).WithMessage("Пароль должен содержать не менее 6 символов.")
            .Matches(@"[A-Z]").WithMessage("Пароль должен содержать хотя бы одну заглавную букву.")
            .Matches(@"[a-z]").WithMessage("Пароль должен содержать хотя бы одну строчную букву.")
            .Matches(@"[0-9]").WithMessage("Пароль должен содержать хотя бы одну цифру.");

        RuleFor(request => request.Email)
            .NotEmpty().WithMessage("Электронная почта не может быть пустой.")
            .EmailAddress().WithMessage("Недопустимый формат электронной почты.");
    }
}