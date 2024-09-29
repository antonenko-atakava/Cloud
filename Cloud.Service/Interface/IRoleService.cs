using Cloud.Domain.Http.Request.Role;
using Cloud.Domain.Http.Response.Role;

namespace Cloud.Service.Interface;

public interface IRoleService
{
    Task<BaseRoleResponse> Get(Guid id);
    Task<bool> Create(CreateRoleRequest request);
    Task<bool> Delete(DeleteRoleRequest request);
}