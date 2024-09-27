using Cloud.DAL.Database.Interface;
using Cloud.Domain.Entity;
using Cloud.Domain.Http.Request.UserPolicy;
using Cloud.Service.Interface;
using Microsoft.Extensions.Logging;

namespace Cloud.Service.Service;

public class UserPolicyService : IUserPolicyService
{
    private readonly IUserPolicyRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly IPolicyRepository _policyRepository;
    private readonly ILogger<UserPolicyService> _logger;

    public UserPolicyService(IUserPolicyRepository repository, IUserRepository userRepository,
        ILogger<UserPolicyService> logger, IPolicyRepository policyRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
        _logger = logger;
        _policyRepository = policyRepository;
    }

    public async Task<bool> Create(CreateUserPolicyRequest request)
    {
        var user = await _userRepository.Get(request.UserId);

        if (user == null)
        {
            _logger.LogError($"[User Policy Service || Create]: Пользователь с ID {request.UserId} не найден");
            throw new Exception($"[User Policy Service || Create]: Пользователь с ID {request.UserId} не найден");
        }

        var policy = await _policyRepository.Get(request.PolicyId);

        if (policy == null)
        {
            _logger.LogError($"[User Policy Service || Create]: Роль с ID {request.UserId} не найден");
            throw new Exception($"[User Policy Service || Create]: Роль с ID {request.UserId} не найден");
        }

        var userPolicy = new UserPolicy
        {
            PolicyId = request.PolicyId,
            UserId = request.UserId
        };

        await _repository.Create(userPolicy);
        await _repository.SaveAsync();

        return true;
    }

    public async Task<bool> Delete(DeleteUserPolicyRequest request)
    {
        var user = await _userRepository.Get(request.UserId);

        if (user == null)
        {
            _logger.LogError($"[User Policy Service || Create]: Пользователь с ID {request.UserId} не найден");
            throw new Exception($"[User Policy Service || Create]: Пользователь с ID {request.UserId} не найден");
        }

        var policy = await _policyRepository.Get(request.PolicyId);

        if (policy == null)
        {
            _logger.LogError($"[User Policy Service || Create]: Роль с ID {request.UserId} не найден");
            throw new Exception($"[User Policy Service || Create]: Роль с ID {request.UserId} не найден");
        }

        var userPolicy = await _repository.Get(request.UserId, request.PolicyId);

        if (userPolicy == null)
        {
            _logger.LogError(
                $"[User Policy Service || Create]: Пользователь с ID {request.UserId} не обладает нужными политиками");
            throw new Exception(
                $"[User Policy Service || Create]: Пользователь с ID {request.UserId} не обладает нужными политиками");
        }

        _repository.Delete(userPolicy);
        await _repository.SaveAsync();

        return true;
    }
}