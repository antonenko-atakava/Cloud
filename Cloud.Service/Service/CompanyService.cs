using AutoMapper;
using Cloud.DAL.Database.Interface;
using Cloud.Domain.Entity;
using Cloud.Domain.Http.Request.Company;
using Cloud.Domain.Http.Response.Company;
using Cloud.Service.Interface;

namespace Cloud.Service.Service;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _repository;
    private readonly IMapper _mapper;

    public CompanyService(ICompanyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetCompanyResponse?> Get(Guid id)
    {
        var company = await _repository.Get(id);

        if (company == null)
            throw new Exception("[Company Service || Get] Компании с таким ID не существует");

        return _mapper.Map<GetCompanyResponse>(company);
    }

    public async Task<BaseCompanyResponse?> GetByName(string name)
    {
        var company = await _repository.GetByName(name);

        if (company == null)
            throw new Exception("[Company Service || Get by name] Компании с таким именем не существует");

        return _mapper.Map<BaseCompanyResponse?>(company);
    }

    public async Task<ICollection<BaseCompanyResponse>> SelectAll()
    {
        var companies = await _repository.SelectAll();
        return _mapper.Map<ICollection<BaseCompanyResponse>>(companies);
    }

    public async Task<ICollection<BaseCompanyResponse>> Pagination(uint number, uint size)
    {
        var companies = await _repository.Pagination(number, size);
        return _mapper.Map<ICollection<BaseCompanyResponse>>(companies);
    }

    public async Task<BaseCompanyResponse> Create(CreateCompanyRequest request)
    {
        var company = await _repository.GetByName(request.Name);

        if (company != null)
            throw new Exception("[Company Service || Create] Компании с таким Именем уже существует");

        company = _mapper.Map<Company>(request);

        await _repository.Create(company);
        await _repository.SaveAsync();

        return _mapper.Map<BaseCompanyResponse>(company);
    }

    public async Task<BaseCompanyResponse> Update(UpdateCompanyRequest request)
    {
        var company = await _repository.Get(request.Id);

        if (company == null)
            throw new Exception("[Company Service || update] Компании с таким ID не существует");

        var companyByName = await _repository.GetByName(request.Name);

        if (companyByName != null)
            throw new Exception("[User service || update]: такое название уже занято");

        company.Name = request.Name;

        _repository.Update(company);
        await _repository.SaveAsync();

        return _mapper.Map<BaseCompanyResponse>(company);
    }

    public async Task<bool> Delete(DeleteCompanyRequest request)
    {
        var company = await _repository.Get(request.Id);

        if (company == null)
            throw new Exception("[Company Service || update] Компании с таким ID не существует");

        _repository.Delete(company);
        await _repository.SaveAsync();

        return true;
    }
}