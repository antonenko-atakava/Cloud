using AutoMapper;
using Cloud.Domain.Entity;
using Cloud.Domain.Http.Request.Policy;
using Cloud.Domain.Http.Response.Policy;

namespace Cloud.Service.Mapper;

public class PolicyMapper : Profile
{
    public PolicyMapper()
    {
        CreateMap<Policy, BasePolicyResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        
        CreateMap<CreatePolicyRequest, Policy>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}