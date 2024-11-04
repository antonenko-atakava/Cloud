using Cloud.DAL.Database.Interface;
using Cloud.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Cloud.DAL.Database.Repository;

public class PolicyRepository : IPolicyRepository
{
    private readonly DatabaseContext _db;

    public PolicyRepository(DatabaseContext db)
    {
        _db = db;
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }

    public async Task<Policy?> Get(Guid id)
    {
        return await _db.Policies
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Policy?> GetByName(string name)
    {
        return await _db.Policies
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Name == name);
    }

    public async Task<ICollection<Policy>> SelectAll()
    {
        return await _db.Policies
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Policy> Create(Policy policy)
    {
        await _db.Policies.AddAsync(policy);
        return policy;
    }

    public bool Delete(Policy policy)
    {
        _db.Policies.AddAsync(policy);

        return true;
    }
}