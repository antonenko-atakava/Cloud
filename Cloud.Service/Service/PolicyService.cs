using AutoMapper;
using Cloud.DAL.Database.Interface;
using Cloud.Domain.Entity;
using Cloud.Domain.Http.Request.Policy;
using Cloud.Domain.Http.Response.Policy;
using Cloud.Service.Interface;
using Microsoft.Extensions.Logging;

namespace Cloud.Service.Service;

public class PolicyService : IPolicyService
{
    private readonly IPolicyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<PolicyService> _logger;

    public PolicyService(IPolicyRepository repository, IMapper mapper, ILogger<PolicyService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BasePolicyResponse> Get(Guid id)
    {
        var policy = await _repository.Get(id);

        if (policy == null)
        {
            _logger.LogError($"[Policy Service || Get by ID]: Политика по ID {id} не найдена");
            throw new Exception($"[Policy Service || Get by ID]: Политика по ID {id} не найдена");
        }

        return _mapper.Map<BasePolicyResponse>(policy);
    }

    public async Task<BasePolicyResponse> GetByName(string name)
    {
        var policy = await _repository.GetByName(name);

        if (policy == null)
        {
            _logger.LogError($"[Policy Service || Get by name]: Политика по названию {name} не найдена");
            throw new Exception($"[Policy Service || Get by name]: Политика по названию {name} не найдена");
        }

        return _mapper.Map<BasePolicyResponse>(policy);
    }

    public async Task<ICollection<BasePolicyResponse>> SelectAll()
    {
        var policies = await _repository.SelectAll();
        return _mapper.Map<ICollection<BasePolicyResponse>>(policies);
    }

    public async Task<BasePolicyResponse> Create(CreatePolicyRequest request)
    {
        var policy = await _repository.GetByName(request.Name);

        if (policy != null)
        {
            _logger.LogError(
                $"[Policy Service || Create]: Политика с таким названием уже существует Название политики:({request.Name})");
            throw new Exception(
                $"[Policy Service || Create]: Политика с таким названием уже существует. Название политики:({request.Name})");
        }

        policy = _mapper.Map<Policy>(request);

        await _repository.Create(policy);
        await _repository.SaveAsync();

        return _mapper.Map<BasePolicyResponse>(policy);
    }

    public async Task<bool> Delete(string name)
    {
        var policy = await _repository.GetByName(name);

        if (policy == null)
        {
            _logger.LogError($"[Policy Service || Delete]: Политика по названию {name} не найдена");
            throw new Exception($"[Policy Service || Delete]: Политика по названию {name} не найдена");
        }

        _repository.Delete(policy);
        await _repository.SaveAsync();

        return true;
    }
}