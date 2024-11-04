using Cloud.DAL.Database.Interface;
using Cloud.Domain.Entity;
using Cloud.Domain.Http.Request.RolePolicy;
using Cloud.Service.Interface;

namespace Cloud.Service.Service;

public class RolePolicyService : IRolePolicyService
{
    private readonly IRolePolicyRepository _repository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPolicyRepository _policyRepository;

    public RolePolicyService(IRolePolicyRepository repository, IRoleRepository roleRepository,
        IPolicyRepository policyRepository)
    {
        _repository = repository;
        _roleRepository = roleRepository;
        _policyRepository = policyRepository;
    }

    public async Task<bool> Create(CreateRolePolicyRequest request)
    {
        var role = await _roleRepository.Get(request.RoleId);

        if (role == null)
            throw new Exception($"[Role Policy Service || Create]: Политика с ID {request.RoleId} не найден");

        var policy = await _policyRepository.Get(request.PolicyId);

        if (policy == null)
            throw new Exception($"[Role Policy Service || Create]: Роль с ID {request.RoleId} не найден");

        var rolePolicy = await _repository.Get(request.RoleId, request.PolicyId);

        if (rolePolicy != null)
            throw new Exception(
                $"[Role Policy Service || Create]: Роль с ID {request.RoleId}  обладает выбранной политикой");

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
            throw new Exception($"[Role Policy Service || Delete]: Политика с ID {request.RoleId} не найден");

        var policy = await _policyRepository.Get(request.PolicyId);

        if (policy == null)
            throw new Exception($"[Role Policy Service || Delete]: Роль с ID {request.RoleId} не найден");

        var rolePolicy = await _repository.Get(request.RoleId, request.PolicyId);

        if (rolePolicy == null)
            throw new Exception($"[Role Policy Service || Delete]: Удаляемой связи не существует");

        _repository.Delete(rolePolicy);
        await _repository.SaveAsync();

        return true;
    }
}