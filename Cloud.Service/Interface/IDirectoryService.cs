using Cloud.Domain.Entity;
using Cloud.Domain.Http.Request.Directory;
using Cloud.Domain.Http.Response.Directory;

namespace Cloud.Service.Interface;

public interface IDirectoryService
{
    Task<BaseDirectoryResponse> Get(Guid id);
    Task<BaseDirectoryResponse> GetByPath(string path);
    Task<ICollection<BaseDirectoryResponse>> GetAllUserDirectories(Guid userId);
    Task<ICollection<BaseDirectoryResponse>> GetByName(string name, Guid userId);
    Task<ICollection<BaseDirectoryResponse>> GetSubDirectories(Guid id);
    Task<BaseDirectoryResponse> Create(CreateDirectoryRequest request);
    Task<BaseDirectoryResponse> Rename(UpdateDirectoryRequest request);
    Task<BaseDirectoryResponse> UpdateIcon(UpdateIconDirectoryRequest request);
    Task<BaseDirectoryResponse> Move(UpdatePathDirectoryRequest request);
    Task<bool> Delete(Guid id);
}