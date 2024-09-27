using Cloud.DAL.Database;
using Cloud.DAL.Database.Interface;
using Cloud.DAL.Database.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Cloud.DAL.Extension;

public static class RepositoryCollectionExtension
{
    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IPolicyRepository, PolicyRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserPolicyRepository, UserPolicyRepository>();

        services.AddDbContext<DatabaseContext>();

        return services;
    }
}