using Cloud.Domain.Http.Request.Role;
using FluentValidation;

namespace Cloud.Validator.Role;

public class GetRoleValidator : AbstractValidator<GetRoleRequest>
{
    public GetRoleValidator()
    {
        RuleFor(f => f.Id)
            .NotEmpty()
            .WithMessage("ID не должен быть пустым");
    }
}