using AutoMapper;
using CarSharingApp.CarService.Application.Commands.ImageCommands;
using CarSharingApp.CarService.Application.DTO_s.Image;
using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Domain.Entities;
using CarSharingApp.CarService.Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel.Args;


namespace CarSharingApp.CarService.Application.CommandHandlers.ImageCommandHandlers;

public class CreateImageHandler : ICommandHandler<CreateImageCommand, ImageDto>
{
    private readonly MinioClient _minioClient;
    private readonly ICarImageRepository _carImageRepository;
    private readonly IMapper _mapper;
    private string _bucketName;
    
    public CreateImageHandler(MinioClient minioClient, IConfiguration configuration, ICarImageRepository carImageRepository, IMapper mapper)
    {
        _minioClient = minioClient;
        _carImageRepository = carImageRepository;
        _mapper = mapper;
        _bucketName = configuration["MinIO-Settings:BucketName"];
    }

    public async Task<ImageDto> Handle(CreateImageCommand command)
    {
        var putObjectArgs = new PutObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(command.Url)
            .WithStreamData(command.File)
            .WithObjectSize(command.File.Length)
            .WithContentType("image/jpeg");
        
        var result =  await _minioClient.PutObjectAsync(putObjectArgs);

        if (result == null)
        {
            throw new InvalidDataTypeException("Invalid Data Type");
        }
        
        var carImage = _mapper.Map<CarImage>(command);
        var newCarImage = await _carImageRepository.AddAsync(carImage);
        var imageDto = _mapper.Map<ImageDto>(newCarImage);
        
        return imageDto;
    }
}