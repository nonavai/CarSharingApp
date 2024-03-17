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
                        }))
            .ForMember(dest => dest.Comments,
                opt =>
                    opt.MapFrom(src => src.Comments.Select(comment =>
                        new CommentDto
                        {
                            Id = comment.Id,
                            CarId = comment.CarId,
                            UserId = comment.UserId,
                            TimePosted = comment.TimePosted,
                            Text = comment.Text,
                            Rating = comment.Rating
                        }))).ReverseMap()
            .ForMember(
                dest => dest.Color, 
                opt => 
                    opt.MapFrom(src =>src.Color))
            .ForMember(
                dest => dest.Id, 
                opt => 
                    opt.MapFrom(src =>src.Id))
            .ForMember(
                dest => dest.Description, 
                opt => 
                    opt.MapFrom(src =>src.Description))
            .ForMember(
                dest => dest.Mark, 
                opt => 
                    opt.MapFrom(src =>src.Mark))
            .ForMember(
                dest => dest.Model, 
                opt => 
                    opt.MapFrom(src =>src.Model))
            .ForMember(
                dest => dest.Price, 
                opt => 
                    opt.MapFrom(src =>src.Price))
            .ForMember(
                dest => dest.UserId, 
                opt => 
                    opt.MapFrom(src =>src.UserId))
            .ForMember(
                dest => dest.Year, 
                opt =>
                    opt.MapFrom(src =>src.Year))
            .ForMember(
                dest => dest.EngineCapacity, 
                opt =>
                    opt.MapFrom(src =>src.EngineCapacity))
            .ForMember(
                dest => dest.FuelType, 
                opt =>
                    opt.MapFrom(src =>src.FuelType))
            .ForMember(
                dest => dest.RegistrationNumber, 
                opt =>
                    opt.MapFrom(src =>src.RegistrationNumber))
            .ForMember(
                dest => dest.VehicleType, 
                opt =>
                    opt.MapFrom(src =>src.VehicleType))
            .ForMember(
                dest => dest.WheelDrive, 
                opt =>
                    opt.MapFrom(src =>src.WheelDrive));
        
        CreateMap<CreateCarCommand, Car>()
            .ForMember(
                dest => dest.Color, 
                opt => 
                    opt.MapFrom(src =>src.Color))
            .ForMember(
                dest => dest.Description, 
                opt => 
                    opt.MapFrom(src =>src.Description))
            .ForMember(
                dest => dest.Mark, 
                opt => 
                    opt.MapFrom(src =>src.Mark))
            .ForMember(
                dest => dest.Model, 
                opt => 
                    opt.MapFrom(src =>src.Model))
            .ForMember(
                dest => dest.Price, 
                opt => 
                    opt.MapFrom(src =>src.Price))
            .ForMember(
                dest => dest.UserId, 
                opt => 
                    opt.MapFrom(src =>src.UserId))
            .ForMember(
                dest => dest.Year, 
                opt =>
                    opt.MapFrom(src =>src.Year))
            .ForMember(
                dest => dest.EngineCapacity, 
                opt =>
                    opt.MapFrom(src =>src.EngineCapacity))
            .ForMember(
                dest => dest.FuelType, 
                opt =>
                    opt.MapFrom(src =>src.FuelType))
            .ForMember(
                dest => dest.RegistrationNumber, 
                opt =>
                    opt.MapFrom(src =>src.RegistrationNumber))
            .ForMember(
                dest => dest.VehicleType, 
                opt =>
                    opt.MapFrom(src =>src.VehicleType))
            .ForMember(
                dest => dest.WheelDrive, 
                opt =>
                    opt.MapFrom(src =>src.WheelDrive));
        
        CreateMap<UpdateCarCommand, Car>().ForMember(
                dest => dest.Color, 
                opt => 
                    opt.MapFrom(src =>src.Color))
            .ForMember(
                dest => dest.Id, 
                opt => 
                    opt.MapFrom(src =>src.Id))
            .ForMember(
                dest => dest.Description, 
                opt => 
                    opt.MapFrom(src =>src.Description))
            .ForMember(
                dest => dest.Mark, 
                opt => 
                    opt.MapFrom(src =>src.Mark))
            .ForMember(
                dest => dest.Model, 
                opt => 
                    opt.MapFrom(src =>src.Model))
            .ForMember(
                dest => dest.Price, 
                opt => 
                    opt.MapFrom(src =>src.Price))
            .ForMember(
                dest => dest.Year, 
                opt =>
                    opt.MapFrom(src =>src.Year))
            .ForMember(
                dest => dest.EngineCapacity, 
                opt =>
                    opt.MapFrom(src =>src.EngineCapacity))
            .ForMember(
                dest => dest.FuelType, 
                opt =>
                    opt.MapFrom(src =>src.FuelType))
            .ForMember(
                dest => dest.RegistrationNumber, 
                opt =>
                    opt.MapFrom(src =>src.RegistrationNumber))
            .ForMember(
                dest => dest.VehicleType, 
                opt =>
                    opt.MapFrom(src =>src.VehicleType))
            .ForMember(
                dest => dest.WheelDrive, 
                opt =>
                    opt.MapFrom(src =>src.WheelDrive));
        
        CreateMap<Car, CarDto>()
            .ForMember(
                dest => dest.Color,
                opt =>
                    opt.MapFrom(src => src.Color))
            .ForMember(
                dest => dest.Id,
                opt =>
                    opt.MapFrom(src => src.Id))
            .ForMember(
                dest => dest.Description,
                opt =>
                    opt.MapFrom(src => src.Description))
            .ForMember(
                dest => dest.Mark,
                opt =>
                    opt.MapFrom(src => src.Mark))
            .ForMember(
                dest => dest.Model,
                opt =>
                    opt.MapFrom(src => src.Model))
            .ForMember(
                dest => dest.Price,
                opt =>
                    opt.MapFrom(src => src.Price))
            .ForMember(
                dest => dest.UserId,
                opt =>
                    opt.MapFrom(src => src.UserId))
            .ForMember(
                dest => dest.Year,
                opt =>
                    opt.MapFrom(src => src.Year))
            .ForMember(
                dest => dest.EngineCapacity,
                opt =>
                    opt.MapFrom(src => src.EngineCapacity))
            .ForMember(
                dest => dest.FuelType,
                opt =>
                    opt.MapFrom(src => src.FuelType))
            .ForMember(
                dest => dest.RegistrationNumber,
                opt =>
                    opt.MapFrom(src => src.RegistrationNumber))
            .ForMember(
                dest => dest.VehicleType,
                opt =>
                    opt.MapFrom(src => src.VehicleType))
            .ForMember(
                dest => dest.WheelDrive,
                opt =>
                    opt.MapFrom(src => src.WheelDrive));
            
        CreateMap<Car, CarWithImageDto>()
            .ForMember(
                dest => dest.Color,
                opt =>
                    opt.MapFrom(src => src.Color))
            .ForMember(
                dest => dest.Id,
                opt =>
                    opt.MapFrom(src => src.Id))
            .ForMember(
                dest => dest.Description,
                opt =>
                    opt.MapFrom(src => src.Description))
            .ForMember(
                dest => dest.Mark,
                opt =>
                    opt.MapFrom(src => src.Mark))
            .ForMember(
                dest => dest.Model,
                opt =>
                    opt.MapFrom(src => src.Model))
            .ForMember(
                dest => dest.Price,
                opt =>
                    opt.MapFrom(src => src.Price))
            .ForMember(
                dest => dest.UserId,
                opt =>
                    opt.MapFrom(src => src.UserId))
            .ForMember(
                dest => dest.Year,
                opt =>
                    opt.MapFrom(src => src.Year))
            .ForMember(
                dest => dest.EngineCapacity,
                opt =>
                    opt.MapFrom(src => src.EngineCapacity))
            .ForMember(
                dest => dest.FuelType,
                opt =>
                    opt.MapFrom(src => src.FuelType))
            .ForMember(
                dest => dest.RegistrationNumber,
                opt =>
                    opt.MapFrom(src => src.RegistrationNumber))
            .ForMember(
                dest => dest.VehicleType,
                opt =>
                    opt.MapFrom(src => src.VehicleType))
            .ForMember(
                dest => dest.WheelDrive,
                opt =>
                    opt.MapFrom(src => src.WheelDrive));

        CreateMap<UpdateCarStatusCommand, CarState>()
            .ForMember(
                dest => dest.Status,
                opt =>
                    opt.MapFrom(src => src.Status))
            .ForMember(
                dest => dest.CarId,
                opt =>
                    opt.MapFrom(src => src.CarId));
        CreateMap<UpdateCarLocationCommand, CarState>()
            .ForMember(
                dest => dest.Latitude,
                opt =>
                    opt.MapFrom(src => src.Latitude))
            .ForMember(
                dest => dest.Longitude,
                opt =>
                    opt.MapFrom(src => src.Longitude))
            .ForMember(
                dest => dest.CarId,
                opt =>
                    opt.MapFrom(src => src.CarId));
        CreateMap<CarState, CarStateDto>()
            .ForMember(
                dest => dest.Latitude,
                opt =>
                    opt.MapFrom(src => src.Latitude))
            .ForMember(
                dest => dest.Status,
                opt =>
                    opt.MapFrom(src => src.Status))
            .ForMember(
                dest => dest.Longitude,
                opt =>
                    opt.MapFrom(src => src.Longitude))
            .ForMember(
                dest => dest.CarId,
                opt =>
                    opt.MapFrom(src => src.CarId))
            .ForMember(
                dest => dest.Id,
                opt =>
                    opt.MapFrom(src => src.Id));

        CreateMap<CreateCommentCommand, Comment>()
            .ForMember(
                dest => dest.Rating,
                opt =>
                    opt.MapFrom(src => src.Rating))
            .ForMember(
                dest => dest.Text,
                opt =>
                    opt.MapFrom(src => src.Text))
            .ForMember(
                dest => dest.UserId,
                opt =>
                    opt.MapFrom(src => src.UserId))
            .ForMember(
                dest => dest.CarId,
                opt =>
                    opt.MapFrom(src => src.CarId));
        CreateMap<UpdateCommentCommand, Comment>()
            .ForMember(
                dest => dest.Rating,
                opt =>
                    opt.MapFrom(src => src.Rating))
            .ForMember(
                dest => dest.Text,
                opt =>
                    opt.MapFrom(src => src.Text))
            .ForMember(
                dest => dest.Id,
                opt =>
                    opt.MapFrom(src => src.Id));
        CreateMap<Comment, CommentDto>()
            .ForMember(
                dest => dest.Rating,
                opt =>
                    opt.MapFrom(src => src.Rating))
            .ForMember(
                dest => dest.Text,
                opt =>
                    opt.MapFrom(src => src.Text))
            .ForMember(
                dest => dest.TimePosted,
                opt =>
                    opt.MapFrom(src => src.TimePosted))
            .ForMember(
                dest => dest.Rating,
                opt =>
                    opt.MapFrom(src => src.Rating))
            .ForMember(
                dest => dest.UserId,
                opt =>
                    opt.MapFrom(src => src.UserId))
            .ForMember(
                dest => dest.Id,
                opt =>
                    opt.MapFrom(src => src.Id));

        CreateMap<CreateImageCommand, CarImage>()
            .ForMember(
                dest => dest.Url,
                opt =>
                    opt.MapFrom(src => src.Url))
            .ForMember(
                dest => dest.CarId,
                opt =>
                    opt.MapFrom(src => src.CarId))
            .ForMember(
                dest => dest.IsPrimary,
                opt =>
                    opt.MapFrom(src => src.IsPrimary));
        CreateMap<CarImage, ImageDto>()
            .ForMember(
                dest => dest.Url,
                opt =>
                    opt.MapFrom(src => src.Url))
            .ForMember(
                dest => dest.CarId,
                opt =>
                    opt.MapFrom(src => src.CarId))
            .ForMember(
                dest => dest.IsPrimary,
                opt =>
                    opt.MapFrom(src => src.IsPrimary))
            .ForMember(
                dest => dest.Id,
                opt =>
                    opt.MapFrom(src => src.Id));
        CreateMap<CreateImageCommand, ImageCleanDto>()
            .ForMember(dest => dest.File,
                opt =>
                    opt.MapFrom(src => MapFormFileToMemoryStream(src.File)))
            .ForMember(
                dest => dest.Url,
                opt =>
                    opt.MapFrom(src => src.Url));
    }
    
    private MemoryStream MapFormFileToMemoryStream(IFormFile formFile)
    {
        var memoryStream = new MemoryStream();
        formFile.CopyTo(memoryStream);
        memoryStream.Position = 0;
        
        return memoryStream;
    }
}