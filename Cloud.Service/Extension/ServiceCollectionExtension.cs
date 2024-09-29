using Cloud.DAL.Database.Interface;
using Cloud.Service.Infrastructure;
using Cloud.Service.Interface;
using Cloud.Service.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Cloud.Service.Extension;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCloudService(this IServiceCollection services)
    {
        services.AddScoped<FileService>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPolicyService, PolicyService>();

        services.AddScoped<IRolePolicyService, RolePolicyService>();
        services.AddScoped<IRoleService, RoleService>();

        services.AddScoped<IUserRoleService, UserRoleService>();

        return services;
    }
}