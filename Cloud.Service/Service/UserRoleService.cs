using Cloud.DAL.Database.Interface;
using Cloud.Domain.Entity;
using Cloud.Domain.Http.Request.UserRole;
using Cloud.Service.Interface;

namespace Cloud.Service.Service;

public class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository _repository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;

    public UserRoleService(IUserRoleRepository repository,
        IRoleRepository roleRepository, IUserRepository userRepository)
    {
        _repository = repository;
        _roleRepository = roleRepository;
        _userRepository = userRepository;
    }

    public async Task<bool> Create(CreateUserRoleRequest request)
    {
        var role = await _roleRepository.Get(request.RoleId);

        if (role == null)
            throw new Exception($"[User Role Service || Create]: Роль с ID {request.RoleId} не найдена");

        var user = await _userRepository.Get(request.UserId);

        if (user == null)
            throw new Exception($"[User Role Service || Create]: Пользователь с ID {request.UserId} не найден");

        var userRole = await _repository.Get(request.UserId, request.RoleId);

        if (userRole != null)
            throw new Exception(
                $"[User Role Service || Create]: Пользователь ID{request.UserId} уже обладает выбранной ролью");

        userRole = new UserRole
        {
            RoleId = request.RoleId,
            UserId = request.UserId,
        };

        await _repository.Create(userRole);
        await _repository.SaveAsync();

        return true;
    }

    public async Task<bool> Delete(DeleteUserRoleRequest request)
    {
        var role = await _roleRepository.Get(request.RoleId);

        if (role == null)
            throw new Exception($"[User Role Service || Delete]: Роль с ID {request.RoleId} не найдена");

        var user = await _userRepository.Get(request.UserId);

        if (user == null)
            throw new Exception($"[User Role Service || Delete]: Пользователь с ID {request.UserId} не найден");
        
        
        var userRole = await _repository.Get(request.UserId, request.RoleId);
        
        if (userRole == null)
            throw new Exception(
                $"[User Role Service || Delete]: Пользователь ID{request.UserId} не обладает выбранной ролью");

        _repository.Delete(userRole);
        await _repository.SaveAsync();

        return true;
    }
}