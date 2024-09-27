using Cloud.DAL.Database.Interface;
using Cloud.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Cloud.DAL.Database.Repository;

public class UserPolicyRepository : IUserPolicyRepository
{
    private readonly DatabaseContext _db;

    public UserPolicyRepository(DatabaseContext db)
    {
        _db = db;
    }

    public async Task<UserPolicy?> Get(Guid userId, Guid policyId)
    {
        return await _db.UserPolicies
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(i => i.UserId == userId && i.PolicyId == policyId);
    }

    public async Task<UserPolicy> Create(UserPolicy entity)
    {
        await _db.UserPolicies.AddAsync(entity);

        return entity;
    }

    public bool Delete(UserPolicy entity)
    {
        _db.UserPolicies.Remove(entity);

        return true;
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}