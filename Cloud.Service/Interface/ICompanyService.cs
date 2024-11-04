using Cloud.Domain.Http.Request.Company;
using Cloud.Domain.Http.Response.Company;

namespace Cloud.Service.Interface;

public interface ICompanyService
{
    Task<GetCompanyResponse?> Get(Guid id);
    Task<BaseCompanyResponse?> GetByName(string name);
    Task<ICollection<BaseCompanyResponse>> SelectAll();
    Task<ICollection<BaseCompanyResponse>> Pagination(uint number, uint size);
    Task<BaseCompanyResponse> Create(CreateCompanyRequest company);
    Task<BaseCompanyResponse> Update(UpdateCompanyRequest company);
    Task<bool> Delete(DeleteCompanyRequest company);
}