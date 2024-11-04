using Cloud.DAL.Database.Interface;
using Cloud.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Cloud.DAL.Database.Repository;

public class UserRoleRepository : IUserRoleRepository
{
    private readonly DatabaseContext _db;

    public UserRoleRepository(DatabaseContext db)
    {
        _db = db;
    }

    public async Task<UserRole?> Get(Guid userId, Guid roleId)
    {
        return await _db.UserRoles
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.UserId == userId && i.RoleId == roleId);
    }

    public async Task<UserRole> Create(UserRole userRole)
    {
        await _db.UserRoles.AddAsync(userRole);

        return userRole;
    }

    public bool Delete(UserRole userRole)
    {
        _db.UserRoles.Remove(userRole);
        return true;
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}