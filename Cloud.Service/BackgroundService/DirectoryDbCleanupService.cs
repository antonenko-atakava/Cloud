using Cloud.DAL.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cloud.Service.BackgroundService;

public class DirectoryDbCleanupService : Microsoft.Extensions.Hosting.BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DirectoryDbCleanupService> _logger;
    private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(1);

    public DirectoryDbCleanupService(IServiceProvider serviceProvider, ILogger<DirectoryDbCleanupService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await CheckDirectoriesAsync();
            await Task.Delay(_checkInterval, stoppingToken);
        }
    }

    private async Task CheckDirectoriesAsync()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

            var directories = await db.Directories.ToListAsync();

            foreach (var directory in directories)
            {
                if (!Directory.Exists(directory.Path))
                {
                    db.Directories.Remove(directory);
                    _logger.LogWarning(
                        $"Directory {directory.Path} не существует на файловой системе и будет удалена из БД.");
                }
            }

            await db.SaveChangesAsync();
        }
    }
}