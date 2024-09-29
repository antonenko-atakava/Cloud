using Cloud.Domain.Http.Request.RolePolicy;

namespace Cloud.Service.Interface;

public interface IRolePolicyService
{
    Task<bool> Create(CreateRolePolicyRequest request);
    Task<bool> Delete(DeleteRolePolicyRequest request);
}