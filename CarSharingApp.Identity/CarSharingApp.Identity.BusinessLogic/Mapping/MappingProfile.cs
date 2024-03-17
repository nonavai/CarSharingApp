using AutoMapper;
using CarSharingApp.Identity.BusinessLogic.Models.Role;
using CarSharingApp.Identity.BusinessLogic.Models.User;
using CarSharingApp.Identity.BusinessLogic.Models.UserInfo;
using CarSharingApp.Identity.DataAccess.Entities;

namespace CarSharingApp.Identity.BusinessLogic.Mapping;

public class MappingProfileApi : Profile
{
    public MappingProfileApi()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.UserInfo, opt=> opt.MapFrom(src=> src.UserInfo))
            .ReverseMap();
        CreateMap<UserInfo, UserInfoDto>()
            .ForMember(dest => dest.User, opt=> opt.MapFrom(src=> src.User))
            .ReverseMap();
        CreateMap<string, RoleDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src));

        CreateMap<User, UserAuthorizedDto>().ReverseMap();
        CreateMap<User, UserCleanDto>().ReverseMap();
        CreateMap<User, UserNecessaryDto>().ReverseMap();
        CreateMap<UserInfo, UserInfoCleanDto>().ReverseMap();
    }
}