using Microsoft.AspNetCore.Http;

namespace Cloud.Service.Infrastructure;

public class FileService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FileService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> FileSaver(IFormFile file, string path)
    {
        if (file.Length == 0)
            return "[File service || File saver]: Файл не выбран для загрузки";

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        using var stream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(stream);

        string host = _httpContextAccessor.HttpContext.Request.Host.Value;
        Console.WriteLine($"{host}/image/{path}/{file.FileName}");

        return $"{host}/image/{path}/{file.FileName}";
    }
}