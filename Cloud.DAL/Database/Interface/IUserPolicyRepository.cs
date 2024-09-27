using Cloud.Domain.Entity;

namespace Cloud.DAL.Database.Interface;

public interface IUserPolicyRepository : IBaseRepository
{
    Task<UserPolicy?> Get(Guid userId, Guid policyId);
    Task<UserPolicy> Create(UserPolicy entity);
    bool Delete(UserPolicy entity);
}