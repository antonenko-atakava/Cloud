using Cloud.Domain.Http.Request.Directory;
using FluentValidation;

namespace Cloud.Validator.Directory;

public class GetAllUserDirectoryRequestValidator : AbstractValidator<GetAllUserDirectoryRequest>
{
    public GetAllUserDirectoryRequestValidator()
    {
        RuleFor(x => x.userId)
            .NotEmpty()
            .WithMessage("UserId не должен быть пустым.");
    }
}