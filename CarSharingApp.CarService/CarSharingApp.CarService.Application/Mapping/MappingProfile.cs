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
using Microsoft.AspNetCore.Http;

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
                            Status = src.CarState.Status,
                            Latitude = src.CarState.Latitude,
                            Longitude = src.CarState.Longitude
                        }));
        CreateMap<CreateCarCommand, Car>();
        CreateMap<UpdateCarCommand, Car>();   
        CreateMap<Car, CarDto>();
        CreateMap<Car, CarWithImageDto>()
            .ForMember(dest => dest.CarState,
                opt =>
                    opt.MapFrom(src =>
                        new CarStateDto
                        {
                            Id = src.CarState.Id,
                            CarId = src.CarState.CarId,
                            Status = src.CarState.Status,
                            Latitude = src.CarState.Latitude,
                            Longitude = src.CarState.Longitude
                        }));;

        CreateMap<UpdateCarStatusCommand, CarState>();
        CreateMap<UpdateCarLocationCommand, CarState>();
        CreateMap<Car, CarStateDto>();
        CreateMap<CarState, CarStateDto>();

        CreateMap<CreateCommentCommand, Comment>();
        CreateMap<UpdateCommentCommand, Comment>();
        CreateMap<Comment, CommentDto>();

        CreateMap<CreateImageCommand, CarImage>();
        CreateMap<CarImage, ImageDto>();
        CreateMap<CarImage, ImageFullDto>().ReverseMap();
        CreateMap<CreateImageCommand, ImageCleanDto>()
            .ForMember(dest=> dest.File,
                opt=>
                    opt.MapFrom(src => MapFormFileToMemoryStream(src.File)));
    }
    
    private MemoryStream MapFormFileToMemoryStream(IFormFile formFile)
    {
        var memoryStream = new MemoryStream();
        formFile.CopyTo(memoryStream);
        memoryStream.Position = 0;
        
        return memoryStream;
    }
}