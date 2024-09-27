using Cloud.Domain.Http.Request.Policy;
using Cloud.Domain.Http.Response.Policy;

namespace Cloud.Service.Interface;

public interface IPolicyService
{
    Task<BasePolicyResponse> Get(Guid id);
    Task<BasePolicyResponse> GetByName(string name);
    Task<ICollection<BasePolicyResponse>> SelectAll();
    Task<BasePolicyResponse> Create(CreatePolicyRequest request);
    Task<bool> Delete(string name);
}