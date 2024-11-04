using AutoMapper;
using Cloud.Domain.Entity;
using Cloud.Domain.Http.Request.Directory;
using Cloud.Domain.Http.Response.Directory;

namespace Cloud.Service.Mapper;

public class DirectoryMapper : Profile
{
    public DirectoryMapper()
    {
        CreateMap<CustomDirectory, BaseDirectoryResponse>()
            .ForMember(dest => dest.ChildrenCategories,
                opt
                    => opt.MapFrom(src => src
                        .ChildrenCategories!
                        .Select(child => new ChildDirectoryResponse
                        {
                            Id = child.Id,
                            Name = child.Name,
                            Icon = child.Icon,
                            Path = child.Path
                        })))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner));

        CreateMap<CreateDirectoryRequest, CustomDirectory>()
            .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => "BaseIconDirectory"))
            .ForMember(dest => dest.AtCreate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.AtUpdate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ChildrenCategories, opt => opt.Ignore())
            .ForMember(dest => dest.ParentDirectory, opt => opt.Ignore())
            .ForMember(dest => dest.Owner, opt => opt.Ignore())
            .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
            .ForMember(dest => dest.PathParentDirectory, opt => opt.MapFrom(src => src.Path))
            .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Path + '/' + src.Name));

        CreateMap<UpdateDirectoryRequest, CustomDirectory>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.AtUpdate, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<UpdateIconDirectoryRequest, CustomDirectory>()
            .ForMember(dest => dest.Icon, opt => opt.Ignore())
            .ForMember(dest => dest.AtUpdate, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<UpdatePathDirectoryRequest, CustomDirectory>()
            .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.NewPath))
            .ForMember(dest => dest.AtUpdate, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<GetByNameDirectoryRequest, CustomDirectory>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NameDirectory));

        CreateMap<GetDirectoryRequest, CustomDirectory>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        CreateMap<GetByPathDirectoryRequest, CustomDirectory>()
            .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Path));
    }
}