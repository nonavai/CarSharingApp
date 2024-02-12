using AutoMapper;
using CarSharingApp.CarService.Application.Commands.ImageCommands;
using CarSharingApp.CarService.Application.DTO_s.Image;
using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Domain.Entities;
using CarSharingApp.CarService.Domain.Exceptions;
using MediatR;


namespace CarSharingApp.CarService.Application.CommandHandlers.ImageCommandHandlers;

public class CreateImageHandler : IRequestHandler<CreateImageCommand, ImageDto>
{
    private readonly IMinioRepository _minioRepository;
    private readonly ICarImageRepository _carImageRepository;
    private readonly IMapper _mapper;

    public CreateImageHandler(ICarImageRepository carImageRepository, IMapper mapper, IMinioRepository minioRepository)
    {
        _carImageRepository = carImageRepository;
        _mapper = mapper;
        _minioRepository = minioRepository;
    }

    public async Task<ImageDto> Handle(CreateImageCommand command, CancellationToken cancellationToken)
    {
        var image = _mapper.Map<ImageCleanDto>(command);
        var response = await _minioRepository.AddAsync(image, cancellationToken);

        if (response == null)
        {
            throw new InvalidDataTypeException("Invalid Data Type");
        }
        
        var carImage = _mapper.Map<CarImage>(command);
        var newCarImage = await _carImageRepository.AddAsync(carImage, cancellationToken);
        await _carImageRepository.SaveChangesAsync(cancellationToken);
        var imageDto = _mapper.Map<ImageDto>(newCarImage);
        
        return imageDto;
    }
}