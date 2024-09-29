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
        
        services.AddScoped<IRolePolicyRepository, RolePolicyRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();

        services.AddDbContext<DatabaseContext>();

        return services;
    }
}