using Cloud.Service.BackgroundService;
using Microsoft.Extensions.DependencyInjection;

namespace Cloud.Service.Extension;

public static class BackgroundServiceExtension
{
    public static IServiceCollection BackgroundServiceCollection(this IServiceCollection services)
    {
        services.AddHostedService<DirectoryCleanupService>();
        services.AddHostedService<DirectoryDbCleanupService>();
        
        return services;
    }
}