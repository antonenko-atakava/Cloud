using Cloud.Domain.Http.Request;
using Cloud.Domain.Http.Request.Company;
using Cloud.Domain.Http.Request.Directory;
using Cloud.Domain.Http.Request.Policy;
using Cloud.Domain.Http.Request.Role;
using Cloud.Domain.Http.Request.RolePolicy;
using Cloud.Domain.Http.Request.User;
using Cloud.Domain.Http.Request.UserRole;
using Cloud.Validator;
using Cloud.Validator.Company;
using Cloud.Validator.Directory;
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
        services.AddScoped<IValidator<GetByNameUserRequest>, GetByNameUserValidator>();
        services.AddScoped<IValidator<GetByEmailUserRequest>, GetByEmailUserValidator>();
        services.AddScoped<IValidator<GetByPhoneUserRequest>, GetByPhoneUserValidator>();
        services.AddScoped<IValidator<UpdateUserRequest>, UpdateUserValidator>();

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

        services.AddScoped(typeof(IValidator<CreateCompanyRequest>), typeof(CreateCompanyValidator));
        services.AddScoped(typeof(IValidator<DeleteCompanyRequest>), typeof(DeleteCompanyValidator));
        services.AddScoped(typeof(IValidator<UpdateCompanyRequest>), typeof(UpdateCompanyValidator));
        services.AddScoped(typeof(IValidator<GetCompanyRequest>), typeof(GetCompanyValidator));
        services.AddScoped(typeof(IValidator<GetByNameCompanyRequest>), typeof(GetByNameCompanyValidator));
        
        services.AddScoped<IValidator<PaginationRequest>, PaginationValidator>();

        services.AddScoped<IValidator<CreateDirectoryRequest>, CreateDirectoryValidator>();
        services.AddScoped<IValidator<GetByNameDirectoryRequest>, GetByNameDirectoryRequestValidator>();
        services.AddScoped<IValidator<GetByPathDirectoryRequest>, GetByPathDirectoryValidator>();
        services.AddScoped<IValidator<GetDirectoryRequest>, GetDirectoryValidator>();
        services.AddScoped<IValidator<GetSubDirectoriesRequest>, GetSubDirectoriesRequestValidator>();
        services.AddScoped<IValidator<GetAllUserDirectoryRequest>, GetAllUserDirectoryRequestValidator>();
        services.AddScoped<IValidator<GetByNameDirectoryRequest>, GetByNameDirectoryRequestValidator>();
        services.AddScoped<IValidator<GetByUserDirectoryRequest>, GetByUserDirectoryRequestValidator>();
        services.AddScoped<IValidator<UpdateDirectoryRequest>, UpdateDirectoryValidator>();
        services.AddScoped<IValidator<UpdateIconDirectoryRequest>, UpdateIconDirectoryValidator>();
        services.AddScoped<IValidator<UpdatePathDirectoryRequest>, UpdatePathDirectoryValidator>();
        
        return services;
    }
}