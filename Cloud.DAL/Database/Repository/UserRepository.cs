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
            .Include(i => i.UserRoles)!
            .ThenInclude(i => i.Role)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<User?> GetByName(string name)
    {
        return await _db.Users
            .Include(i => i.UserRoles)!
            .ThenInclude(i => i.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Login == name);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _db.Users
            .Include(i => i.UserRoles)!
            .ThenInclude(i => i.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User?> GetByPhone(string phone)
    {
        return await _db.Users
            .Include(i => i.UserRoles)!
            .ThenInclude(i => i.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Phone == phone);
    }

    public async Task<IEnumerable<User>> SelectAll()
    {
        return await _db.Users
            .Include(i => i.UserRoles)!
            .ThenInclude(i => i.Role)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> Pagination(uint number, uint size)
    {
        return await _db.Users
            .Skip(((int)number - 1) * (int)size)
            .Take((int)size)
            .Include(i => i.UserRoles)!
            .ThenInclude(i => i.Role)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<User> Create(User user)
    {
        await _db.Users.AddAsync(user);
        return user;
    }

    public async Task<User?> Login(string login, string password)
    {
        return await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Login == login && i.Password == password);
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