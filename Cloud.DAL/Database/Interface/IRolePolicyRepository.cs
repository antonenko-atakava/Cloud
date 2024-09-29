using Cloud.Domain.Entity;

namespace Cloud.DAL.Database.Interface;

public interface IRolePolicyRepository : IBaseRepository
{
    Task<RolePolicy?> Get(Guid roleId, Guid policyId);
    Task<RolePolicy> Add(RolePolicy entity);
    bool Delete(RolePolicy entity);
}