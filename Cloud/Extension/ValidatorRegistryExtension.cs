using Cloud.Domain.Http.Request.Policy;
using Cloud.Domain.Http.Request.Role;
using Cloud.Domain.Http.Request.RolePolicy;
using Cloud.Domain.Http.Request.User;
using Cloud.Domain.Http.Request.UserRole;
using Cloud.Validator.Policy;
using Cloud.Validator.Role;
using Cloud.Validator.RolePolicy;
using Cloud.Validator.User;
using Cloud.Validator.UserRole;
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
        
        services.AddScoped<IValidator<CreateRolePolicyRequest>, CreateRolePolicyValidator>();
        services.AddScoped(typeof(IValidator<DeleteRolePolicyRequest>), typeof(DeleteRolePolicyValidator));

        services.AddScoped(typeof(IValidator<GetRoleRequest>), typeof(GetRoleValidator));
        services.AddScoped(typeof(IValidator<CreateRoleRequest>), typeof(CreateRoleValidator));
        services.AddScoped(typeof(IValidator<DeleteRolePolicyRequest>), typeof(DeleteRolePolicyValidator));

        services.AddScoped(typeof(IValidator<CreateUserRoleRequest>), typeof(CreateUserRoleValidator));
        services.AddScoped(typeof(IValidator<DeleteUserRoleRequest>), typeof(DeleteUserRoleValidator));
        
        return services;
    }
}