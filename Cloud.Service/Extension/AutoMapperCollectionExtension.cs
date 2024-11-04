using Cloud.Service.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Cloud.Service.Extension;

public static class AutoMapperCollectionExtension
{
    public static IServiceCollection AddAutoMapperCollection(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UserMapper), typeof(PolicyMapper), typeof(CompanyMapper), typeof(DirectoryMapper));

        return services;
    }
}