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
            .ForMember(dest => dest.UserInfoDto,
                opt=>
                    opt.MapFrom(src=> src.UserInfo)).ReverseMap()
            .ForMember(
                dest => dest.FirstName,
                opt =>
                    opt.MapFrom(src => src.FirstName))
            .ForMember(
                dest => dest.Email,
                opt =>
                    opt.MapFrom(src => src.Email))
            .ForMember(
                dest => dest.LastName,
                opt =>
                    opt.MapFrom(src => src.LastName))
            .ForMember(
                dest => dest.PhoneNumber,
                opt =>
                    opt.MapFrom(src => src.PhoneNumber))
            .ForMember(
                dest => dest.RecordNumber,
                opt =>
                    opt.MapFrom(src => src.RecordNumber))
            .ForMember(
                dest => dest.UserName,
                opt =>
                    opt.MapFrom(src => src.UserName))
            .ForMember(
                dest => dest.Id,
                opt =>
                    opt.MapFrom(src => src.Id));
        
        CreateMap<UserInfo, UserInfoDto>()
            .ForMember(dest => dest.User,
                opt=>
                    opt.MapFrom(src=> src.User)).ReverseMap()
            .ForMember(
                dest => dest.Birth,
                opt =>
                    opt.MapFrom(src => src.Birth))
            .ForMember(
                dest => dest.Category,
                opt =>
                    opt.MapFrom(src => src.Category))
            .ForMember(
                dest => dest.Country,
                opt =>
                    opt.MapFrom(src => src.Country))
            .ForMember(
                dest => dest.LicenceExpiry,
                opt =>
                    opt.MapFrom(src => src.LicenceExpiry))
            .ForMember(
                dest => dest.LicenceId,
                opt =>
                    opt.MapFrom(src => src.LicenceId))
            .ForMember(
                dest => dest.LicenceIssue,
                opt =>
                    opt.MapFrom(src => src.LicenceIssue))
            .ForMember(
                dest => dest.PlaceOfIssue,
                opt =>
                    opt.MapFrom(src => src.PlaceOfIssue))
            .ForMember(
                dest => dest.UserId,
                opt =>
                    opt.MapFrom(src => src.UserId))
            .ForMember(
                dest => dest.Id,
                opt =>
                    opt.MapFrom(src => src.Id));
        CreateMap<string, RoleDto>()
            .ForMember(dest => dest.Name,
                opt =>
                    opt.MapFrom(src => src));

        CreateMap<User, UserAuthorizedDto>().ReverseMap()
            .ForMember(
                dest => dest.FirstName,
                opt =>
                    opt.MapFrom(src => src.FirstName))
            .ForMember(
                dest => dest.Email,
                opt =>
                    opt.MapFrom(src => src.Email))
            .ForMember(
                dest => dest.LastName,
                opt =>
                    opt.MapFrom(src => src.LastName))
            .ForMember(
                dest => dest.PhoneNumber,
                opt =>
                    opt.MapFrom(src => src.PhoneNumber))
            .ForMember(
                dest => dest.UserName,
                opt =>
                    opt.MapFrom(src => src.UserName))
            .ForMember(
                dest => dest.Id,
                opt =>
                    opt.MapFrom(src => src.Id));
                    
        CreateMap<User, UserCleanDto>().ReverseMap()
            .ForMember(
                dest => dest.FirstName,
                opt =>
                    opt.MapFrom(src => src.FirstName))
            .ForMember(
                dest => dest.Email,
                opt =>
                    opt.MapFrom(src => src.Email))
            .ForMember(
                dest => dest.LastName,
                opt =>
                    opt.MapFrom(src => src.LastName))
            .ForMember(
                dest => dest.PhoneNumber,
                opt =>
                    opt.MapFrom(src => src.PhoneNumber))
            .ForMember(
                dest => dest.UserName,
                opt =>
                    opt.MapFrom(src => src.UserName))
            .ForMember(
                dest => dest.Id,
                opt =>
                    opt.MapFrom(src => src.Id));
        CreateMap<User, UserNecessaryDto>().ReverseMap()
            .ForMember(
                dest => dest.FirstName,
                opt =>
                    opt.MapFrom(src => src.FirstName))
            .ForMember(
                dest => dest.Email,
                opt =>
                    opt.MapFrom(src => src.Email))
            .ForMember(
                dest => dest.LastName,
                opt =>
                    opt.MapFrom(src => src.LastName))
            .ForMember(
                dest => dest.PhoneNumber,
                opt =>
                    opt.MapFrom(src => src.PhoneNumber))
            .ForMember(
                dest => dest.RecordNumber,
                opt =>
                    opt.MapFrom(src => src.RecordNumber))
            .ForMember(
                dest => dest.UserName,
                opt =>
                    opt.MapFrom(src => src.UserName));
        CreateMap<UserInfo, UserInfoCleanDto>().ReverseMap()
            .ForMember(
                dest => dest.Birth,
                opt =>
                    opt.MapFrom(src => src.Birth))
            .ForMember(
                dest => dest.Category,
                opt =>
                    opt.MapFrom(src => src.Category))
            .ForMember(
                dest => dest.Country,
                opt =>
                    opt.MapFrom(src => src.Country))
            .ForMember(
                dest => dest.LicenceExpiry,
                opt =>
                    opt.MapFrom(src => src.LicenceExpiry))
            .ForMember(
                dest => dest.LicenceId,
                opt =>
                    opt.MapFrom(src => src.LicenceId))
            .ForMember(
                dest => dest.LicenceIssue,
                opt =>
                    opt.MapFrom(src => src.LicenceIssue))
            .ForMember(
                dest => dest.PlaceOfIssue,
                opt =>
                    opt.MapFrom(src => src.PlaceOfIssue));
    }
}