using AutoMapper;
using Cloud.Domain.Entity;
using Cloud.Domain.Http.Request.Company;
using Cloud.Domain.Http.Response.Company;

namespace Cloud.Service.Mapper;

public class CompanyMapper : Profile
{
    public CompanyMapper()
    {
        CreateMap<Company, GetCompanyResponse>()
            .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.UserCompany!.Select(uc => uc.User)))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Roles));

        CreateMap<Company, BaseCompanyResponse>();
        
        CreateMap<CreateCompanyRequest, Company>();
        
        CreateMap<UpdateCompanyRequest, Company>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}