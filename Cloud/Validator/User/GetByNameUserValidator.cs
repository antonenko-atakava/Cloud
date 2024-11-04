using Cloud.Domain.Http.Request.User;
using FluentValidation;

namespace Cloud.Validator.User;

public class GetByNameUserValidator : AbstractValidator<GetByNameUserRequest>
{
    public GetByNameUserValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty().WithMessage("Имя не может быть пустым.");
    }
}