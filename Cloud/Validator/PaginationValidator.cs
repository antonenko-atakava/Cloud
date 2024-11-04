using Cloud.Domain.Http.Request;
using FluentValidation;

namespace Cloud.Validator;

public class PaginationValidator : AbstractValidator<PaginationRequest>
{
    public PaginationValidator()
    {
        RuleFor(x => (int)x.Number)
            .GreaterThan(0).WithMessage("Страница не может быть равна 0");

        RuleFor(x => (int)x.Size)
            .InclusiveBetween(5, 25).WithMessage("Вы можете получить на страницу от 5 до 25 товаров");
    }
}