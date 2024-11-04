using Cloud.Domain.Http.Request.User;
using FluentValidation;

namespace Cloud.Validator.User;

public class UpdateAvatarUserValidator : AbstractValidator<UpdateAvatarUserRequest>
{
    private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png" };
    private readonly long _maxFileSize = 500 * 1024 * 1024;

    public UpdateAvatarUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Идентификатор пользователя не должен быть пустым.");

        RuleFor(x => x.File)
            .NotNull().WithMessage("Файл обязателен для загрузки.")
            .Must(IsValidFileType).WithMessage("Недопустимый формат файла. Разрешены только .jpg, .jpeg, .png.")
            .Must(IsValidFileSize).WithMessage("Размер файла не должен превышать 500MB.");
    }

    private bool IsValidFileType(IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName).ToLower();
        return _allowedExtensions.Contains(extension);
    }

    private bool IsValidFileSize(IFormFile file)
    {
        return file.Length <= _maxFileSize;
    }
}