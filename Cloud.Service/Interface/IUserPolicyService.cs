using Cloud.Domain.Http.Request.UserPolicy;

namespace Cloud.Service.Interface;

public interface IUserPolicyService
{
    Task<bool> Create(CreateUserPolicyRequest request);
    Task<bool> Delete(DeleteUserPolicyRequest request);
}