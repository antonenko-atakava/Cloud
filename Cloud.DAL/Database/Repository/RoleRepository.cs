using Cloud.DAL.Database.Interface;
using Cloud.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Cloud.DAL.Database.Repository;

public class RoleRepository : IRoleRepository
{
    private readonly DatabaseContext _db;

    public RoleRepository(DatabaseContext db)
    {
        _db = db;
    }

    public async Task<Role?> Get(Guid id)
    {
        return await _db.Roles
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Role?> GetByName(string name)
    {
        return await _db.Roles
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(i => i.Name == name);
    }

    public async Task<IEnumerable<Role>> SelectAll()
    {
        return await _db.Roles
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync();
    }

    public async Task<Role> Create(Role role)
    {
        await _db.Roles.AddAsync(role);
        return role;
    }

    public bool Delete(Role role)
    {
        _db.Roles.Remove(role);
        return true;
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}