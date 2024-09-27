using Cloud.Domain.Http.Request.Policy;
using Cloud.Domain.Http.Request.User;
using Cloud.Domain.Http.Request.UserPolicy;
using Cloud.Validator.Policy;
using Cloud.Validator.User;
using Cloud.Validator.UserPolicy;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Cloud.Extension;

public static class ValidatorRegistryExtension
{
    public static IServiceCollection AddValidatorRegistry(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        
        services.AddScoped<IValidator<CreateUserRequest>, CreateUserValidator>();
        services.AddScoped<IValidator<GetUserRequest>, GetUserValidator>();

        services.AddScoped<IValidator<CreatePolicyRequest>, CreatePolicyValidator>();
        services.AddScoped(typeof(IValidator<GetPolicyRequest>), typeof(GetPolicyValidator));
        services.AddScoped(typeof(IValidator<GetByNamePolicyRequest>), typeof(GetByNamePolicyValidator));
        services.AddScoped(typeof(IValidator<DeletePolicyRequest>), typeof(DeletePolicyValidator));

        services.AddScoped(typeof(IValidator<CreateUserPolicyRequest>), typeof(CreateUserPolicyValidator));
        services.AddScoped(typeof(IValidator<DeleteUserPolicyRequest>), typeof(DeleteUserPolicyValidator));

        return services;
    }
}