using Cloud.DAL.Database.Interface;
using Cloud.Domain.Entity;
using Cloud.Domain.Http.Request.RolePolicy;
using Cloud.Service.Interface;
using Microsoft.Extensions.Logging;

namespace Cloud.Service.Service;

public class RolePolicyService : IRolePolicyService
{
    private readonly IRolePolicyRepository _repository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPolicyRepository _policyRepository;
    private readonly ILogger<RolePolicyService> _logger;

    public RolePolicyService(IRolePolicyRepository repository, IRoleRepository roleRepository,
        IPolicyRepository policyRepository, ILogger<RolePolicyService> logger)
    {
        _repository = repository;
        _roleRepository = roleRepository;
        _policyRepository = policyRepository;
        _logger = logger;
    }

    public async Task<bool> Create(CreateRolePolicyRequest request)
    {
        var role = await _roleRepository.Get(request.RoleId);

        if (role == null)
        {
            _logger.LogError($"[Role Policy Service || Create]: Политика с ID {request.RoleId} не найден");
            throw new Exception($"[Role Policy Service || Create]: Политика с ID {request.RoleId} не найден");
        }

        var policy = await _policyRepository.Get(request.PolicyId);

        if (policy == null)
        {
            _logger.LogError($"[Role Policy Service || Create]: Роль с ID {request.RoleId} не найден");
            throw new Exception($"[Role Policy Service || Create]: Роль с ID {request.RoleId} не найден");
        }

        var rolePolicy = await _repository.Get(request.RoleId, request.PolicyId);

        if (rolePolicy != null)
        {
            _logger.LogError($"[Role Policy Service || Create]: Роль с ID {request.RoleId} обладает выбранной политикой");
            throw new Exception($"[Role Policy Service || Create]: Роль с ID {request.RoleId}  обладает выбранной политикой");
        }
        
        rolePolicy = new RolePolicy
        {
            PolicyId = request.PolicyId,
            RoleId = request.RoleId,
        };

        await _repository.Add(rolePolicy);
        await _repository.SaveAsync();

        return true;
    }

    public async Task<bool> Delete(DeleteRolePolicyRequest request)
    {
        var role = await _roleRepository.Get(request.RoleId);

        if (role == null)
        {
            _logger.LogError($"[Role Policy Service || Delete]: Политика с ID {request.RoleId} не найден");
            throw new Exception($"[Role Policy Service || Delete]: Политика с ID {request.RoleId} не найден");
        }

        var policy = await _policyRepository.Get(request.PolicyId);

        if (policy == null)
        {
            _logger.LogError($"[Role Policy Service || Delete]: Роль с ID {request.RoleId} не найден");
            throw new Exception($"[Role Policy Service || Delete]: Роль с ID {request.RoleId} не найден");
        }

        var rolePolicy = await _repository.Get(request.RoleId, request.PolicyId);

        if (rolePolicy == null)
        {
            _logger.LogError($"[Role Policy Service || Delete]: Удаляемой связи не существует");
            throw new Exception($"[Role Policy Service || Delete]: Удаляемой связи не существует");
        }
        
        _repository.Delete(rolePolicy);
        await _repository.SaveAsync();
        
        return true;
    }
}