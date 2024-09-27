using AutoMapper;
using Cloud.Domain.Entity;
using Cloud.Domain.Http.Request.User;
using Cloud.Domain.Http.Response.User;

namespace Cloud.Service.Mapper;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, BaseUserResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Login))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.Avatar,
                opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Avatar) ? "Base_Url_Img_User" : src.Avatar));

        CreateMap<CreateUserRequest, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Login))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Phone, opt => opt.Ignore())
            .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => "base_Url_Img_User"))
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.Salt, opt => opt.Ignore())
            .ForMember(dest => dest.Modified, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.Created, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.IsSuperUser, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.UserPolicies, opt => opt.Ignore());;

        CreateMap<UpdateUserRequest, User>()
            .ForAllMembers(options => options.Condition((src, dest, srcMember) => srcMember != null));
    }
}