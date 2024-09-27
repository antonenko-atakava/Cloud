using Cloud.DAL.Database.Interface;
using Cloud.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Cloud.DAL.Database.Repository;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _db;

    public UserRepository(DatabaseContext db)
    {
        _db = db;
    }

    public async Task<User?> Get(Guid id)
    {
        return await _db.Users
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<User?> GetByName(string name)
    {
        return await _db.Users
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(x => x.Login == name);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _db.Users
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User?> GetByPhone(string phone)
    {
        return await _db.Users
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(x => x.Phone == phone);
    }

    public async Task<IEnumerable<User>> SelectAll()
    {
        return await _db.Users
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> Pagination(uint number, uint size)
    {
        return await _db.Users
            .Skip(((int)number * (int)size) - 1)
            .Take((int)size)
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync();
    }

    public async Task<User> Create(User user)
    {
        await _db.Users.AddAsync(user);
        return user;
    }

    public User Update(User user)
    {
        _db.Users.Update(user);
        return user;
    }

    public bool Delete(User user)
    {
        _db.Users.Remove(user);
        return true;
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}