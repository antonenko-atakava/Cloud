using Cloud.Domain.Entity;

namespace Cloud.DAL.Database.Interface;

public interface IRoleRepository : IBaseRepository
{
    Task<Role?> Get(Guid id);
    Task<Role?> GetByName(string name);
    Task<IEnumerable<Role>> SelectAll();
    Task<Role> Create(Role role);
    bool Delete(Role role);
}