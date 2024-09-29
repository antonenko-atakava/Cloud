using Cloud.Domain.Entity;

namespace Cloud.DAL.Database.Interface;

public interface IUserRepository : IBaseRepository
{
    Task<User?> Get(Guid id);
    Task<User?> GetByName(string name);
    Task<User?> GetByEmail(string email);
    Task<User?> GetByPhone(string phone);
    Task<IEnumerable<User>> SelectAll();
    Task<IEnumerable<User>> Pagination(uint number, uint size);
    Task<User> Create(User entity);
    Task<User?> Login(string email, string password);
    User Update(User user);
    bool Delete(User user);
}