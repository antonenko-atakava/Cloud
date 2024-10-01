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
            throw new Exception("[File service || File saver]: Файл не выбран для загрузки");

        // if (!Directory.Exists(path))
        //     Directory.CreateDirectory(path);

        var filePath = Path.Combine(path, file.FileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        string host = _httpContextAccessor.HttpContext.Request.Host.Value;
        return $"{host}/image/{file.FileName}";
    }
}