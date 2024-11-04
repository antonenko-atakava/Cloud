using Cloud.DAL.Database.Interface;
using Cloud.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Cloud.DAL.Database.Repository;

public class RolePolicyRepository : IRolePolicyRepository
{
    private readonly DatabaseContext _db;

    public RolePolicyRepository(DatabaseContext db)
    {
        _db = db;
    }

    public async Task<RolePolicy?> Get(Guid roleId, Guid policyId)
    {
        return await _db.RolePolicies
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.RoleId == roleId && i.PolicyId == policyId);
    }

    public async Task<RolePolicy> Add(RolePolicy entity)
    {
        await _db.RolePolicies.AddAsync(entity);
        return entity;
    }

    public bool Delete(RolePolicy entity)
    {
        _db.RolePolicies.Remove(entity);
        return true;
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}