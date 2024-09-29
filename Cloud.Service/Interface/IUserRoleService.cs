using Cloud.Domain.Http.Request.UserRole;

namespace Cloud.Service.Interface;

public interface IUserRoleService
{
    Task<bool> Create(CreateUserRoleRequest request);
    Task<bool> Delete(DeleteUserRoleRequest request);
}