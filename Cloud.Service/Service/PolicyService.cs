using AutoMapper;
using Cloud.DAL.Database.Interface;
using Cloud.Domain.Entity;
using Cloud.Domain.Http.Request.Policy;
using Cloud.Domain.Http.Response.Policy;
using Cloud.Service.Interface;

namespace Cloud.Service.Service;

public class PolicyService : IPolicyService
{
    private readonly IPolicyRepository _repository;
    private readonly IMapper _mapper;

    public PolicyService(IPolicyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BasePolicyResponse> Get(Guid id)
    {
        var policy = await _repository.Get(id);

        if (policy == null)
            throw new Exception($"[Policy Service || Get by ID]: Политика по ID {id} не найдена");

        return _mapper.Map<BasePolicyResponse>(policy);
    }

    public async Task<BasePolicyResponse> GetByName(string name)
    {
        var policy = await _repository.GetByName(name);

        if (policy == null)
            throw new Exception($"[Policy Service || Get by name]: Политика по названию {name} не найдена");

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
            throw new Exception(
                $"[Policy Service || Create]: Политика с таким названием уже существует. Название политики:({request.Name})");

        policy = _mapper.Map<Policy>(request);

        await _repository.Create(policy);
        await _repository.SaveAsync();

        return _mapper.Map<BasePolicyResponse>(policy);
    }

    public async Task<bool> Delete(string name)
    {
        var policy = await _repository.GetByName(name);

        if (policy == null)
            throw new Exception($"[Policy Service || Delete]: Политика по названию {name} не найдена");

        _repository.Delete(policy);
        await _repository.SaveAsync();

        return true;
    }
}