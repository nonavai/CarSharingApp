using AutoMapper;
using CarSharingApp.CarService.Application.Commands.CarCommands;
using CarSharingApp.CarService.Application.Commands.CarStateCommands;
using CarSharingApp.CarService.Application.Commands.CommentCommands;
using CarSharingApp.CarService.Application.Commands.ImageCommands;
using CarSharingApp.CarService.Application.DTO_s.Car;
using CarSharingApp.CarService.Application.DTO_s.CarState;
using CarSharingApp.CarService.Application.DTO_s.Comment;
using CarSharingApp.CarService.Application.DTO_s.Image;
using CarSharingApp.CarService.Domain.Entities;

namespace CarSharingApp.CarService.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Car, CarFullDto>()
            .ForMember(dest => dest.CarState, 
                opt =>
                    opt.MapFrom(src =>
                        new CarStateDto
                        {
                            Id = src.CarState.Id,
                            CarId = src.CarState.CarId,
                            Latitude = src.CarState.Latitude,
                            Longitude = src.CarState.Longitude
                        }))
            .ForMember(dest => dest.Comments,
                opt =>
                    opt.MapFrom(src => src.Comments.Select(comment =>
                        new CommentDto
                        {
                            Id = comment.Id,
                            UserId = comment.UserId,
                            TimePosted = comment.TimePosted,
                            Text = comment.Text,
                            Rating = comment.Rating
                        }))).ReverseMap();
        CreateMap<CreateCarCommand, Car>();
        CreateMap<UpdateCarCommand, Car>();   
        CreateMap<Car, CarDto>();

        CreateMap<UpdateCarActivityCommand, Car>();
        CreateMap<UpdateCarLocationCommand, Car>();
        CreateMap<Car, CarStateDto>();

        CreateMap<CreateCommentCommand, Comment>();
        CreateMap<UpdateCommentCommand, Comment>();
        CreateMap<Comment, CommentDto>();

        CreateMap<CreateImageCommand, CarImage>();
        CreateMap<CarImage, ImageDto>();
        CreateMap<CreateImageCommand, ImageCleanDto>()
            .ForMember(dest=> dest.File,
                opt=>
                    opt.MapFrom(src => src.File.OpenReadStream()));
    }
}