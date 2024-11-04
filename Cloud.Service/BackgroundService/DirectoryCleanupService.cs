using Cloud.DAL.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cloud.Service.BackgroundService;

public class DirectoryCleanupService : Microsoft.Extensions.Hosting.BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DirectoryCleanupService> _logger;
    private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(1); 
    private readonly IConfiguration _configuration; 

    public DirectoryCleanupService(IServiceProvider serviceProvider, ILogger<DirectoryCleanupService> logger, IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await CheckAndDeleteDirectoriesAsync();
            await Task.Delay(_checkInterval, stoppingToken);
        }
    }

    private async Task CheckAndDeleteDirectoriesAsync()
    {
        var rootDirectoryPath = _configuration["DirectorySettings:RootDirectoryPath"];

        if (string.IsNullOrEmpty(rootDirectoryPath) || !Directory.Exists(rootDirectoryPath))
        {
            _logger.LogWarning("[DirectoryCleanupService]: путь к корневой папке users указан не правильно");
            return;
        }

        using (var scope = _serviceProvider.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            
            var directoryPathsInDb = db.Directories
                .Select(d => d.Path)
                .ToHashSet();
            
            var directoriesOnFileSystem = Directory.GetDirectories(rootDirectoryPath, "*", SearchOption.AllDirectories);

            foreach (var dirPath in directoriesOnFileSystem)
            {
                if (!directoryPathsInDb.Contains(dirPath))
                {
                    try
                    {
                        Directory.Delete(dirPath, true);
                        _logger.LogWarning($"Директория {dirPath} отсутствует в базе данных и была удалена.");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Ошибка при удалении директории {dirPath}: {ex.Message}");
                    }
                }
            }
        }
    }
}