using Cloud.Domain.Entity;

namespace Cloud.DAL.Database.Interface;

public interface IPolicyRepository : IBaseRepository
{
    Task<Policy?> Get(Guid id);
    Task<Policy?> GetByName(string name);
    Task<ICollection<Policy>> SelectAll();
    Task<Policy> Create(Policy policy);
    bool Delete(Policy policy);
}