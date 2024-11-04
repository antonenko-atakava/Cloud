using Cloud.Domain.Entity;

namespace Cloud.DAL.Database.Interface;

public interface ICompanyRepository : IBaseRepository
{
    Task<Company?> Get(Guid id);
    Task<Company?> GetByName(string name);
    Task<ICollection<Company>> SelectAll();
    Task<ICollection<Company>> Pagination(uint number, uint size);
    Task<Company> Create(Company company);
    Company Update(Company company);
    bool Delete(Company company);
}