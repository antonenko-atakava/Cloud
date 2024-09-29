using Cloud.DAL.Database.Interface;
using Cloud.Domain.Entity;
using Cloud.Domain.Http.Request.Role;
using Cloud.Domain.Http.Response.Role;
using Cloud.Service.Interface;
using Microsoft.Extensions.Logging;

namespace Cloud.Service.Service;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _repository;
    private readonly ILogger<RoleService> _logger;

    public RoleService(IRoleRepository repository, ILogger<RoleService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<BaseRoleResponse> Get(Guid id)
    {
        var role = await _repository.Get(id);

        if (role == null)
        {
            _logger.LogError($"[Role || Get]: Роли с Id {id} не существует");
            throw new Exception($"[Role || Get]: Роли с Id {id} не существует");
        }

        var response = new BaseRoleResponse
        {
            Id = role.Id,
            Name = role.Name
        };

        return response;
    }

    public async Task<bool> Create(CreateRoleRequest request)
    {
        var role = await _repository.GetByName(request.Name);

        if (role != null)
        {
            _logger.LogError($"[Role || Create]: Роли с название {request.Name} уже существует");
            throw new Exception($"[Role || Create]: Роли с название {request.Name} уже существует");
        }

        role = new Role()
        {
            Name = request.Name
        };

        await _repository.Create(role);
        await _repository.SaveAsync();

        return true;
    }

    public async Task<bool> Delete(DeleteRoleRequest request)
    {
        var role = await _repository.Get(request.Id);

        if (role == null)
        {
            _logger.LogError($"[Role || Delete]: Роли с название {request.Id} уже существует");
            throw new Exception($"[Role || Delete]: Роли с название {request.Id} уже существует");
        }

        _repository.Delete(role);
        await _repository.SaveAsync();

        return true;
    }
}