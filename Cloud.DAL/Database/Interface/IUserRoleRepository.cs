using Cloud.Domain.Entity;

namespace Cloud.DAL.Database.Interface;

public interface IUserRoleRepository : IBaseRepository
{
    Task<UserRole?> Get(Guid userId, Guid roleId);
    Task<UserRole> Create(UserRole userRole);
    bool Delete(UserRole userRole);
}