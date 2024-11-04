using Cloud.Domain.Entity;

namespace Cloud.DAL.Database.Interface;

public interface IDirectoryRepository : IBaseRepository
{
    Task<CustomDirectory?> Get(Guid id);
    Task<CustomDirectory?> GetByPath(string path);
    Task<ICollection<CustomDirectory>> GetByName(string name, Guid userId);
    Task<ICollection<CustomDirectory>> GetAllUserDirectories(Guid userId);
    Task<ICollection<CustomDirectory>> GetSubDirectories(Guid id);
    Task<CustomDirectory> Create(CustomDirectory entity);
    CustomDirectory Update(CustomDirectory entity);
    Task<bool> Delete(CustomDirectory entity);
}