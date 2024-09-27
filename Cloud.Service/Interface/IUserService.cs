using Cloud.Domain.Http.Request.User;
using Cloud.Domain.Http.Response.User;

namespace Cloud.Service.Interface;

public interface IUserService
{
    Task<BaseUserResponse> Get(Guid id);
    Task<BaseUserResponse> GetByName(string name);
    Task<BaseUserResponse> GetByEmail(string email);
    Task<BaseUserResponse> GetByPhone(string phone);
    Task<ICollection<BaseUserResponse>> SelectAll();
    Task<ICollection<BaseUserResponse>> Pagination(uint number, uint size);
    Task<BaseUserResponse> Create(CreateUserRequest request);
    Task<BaseUserResponse> Update(UpdateUserRequest request);
    Task<BaseUserResponse> UpdateAvatar(UpdateAvatarUserRequest request);
    Task<bool> Delete(DeleteUserRequest request);
}