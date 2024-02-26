using AutoMapper;
using CarSharingApp.CarService.Application.Commands.ImageCommands;
using CarSharingApp.CarService.Application.DTO_s.Image;
using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Domain.Exceptions;
using MediatR;

namespace CarSharingApp.CarService.Application.CommandHandlers.ImageCommandHandlers;

public class DeleteImageHandler : IRequestHandler<DeleteImageCommand, ImageDto>
{
    private readonly IMinioRepository _minioRepository;
    private readonly ICarImageRepository _carImageRepository;
    private readonly IMapper _mapper;

    public DeleteImageHandler(ICarImageRepository carImageRepository, IMapper mapper, IMinioRepository minioRepository)
    {
        _carImageRepository = carImageRepository;
        _mapper = mapper;
        _minioRepository = minioRepository;
    }

    public async Task<ImageDto> Handle(DeleteImageCommand command, CancellationToken cancellationToken)
    {
        var image = await _carImageRepository.GetByIdAsync(command.Id, cancellationToken);

        if (image == null)
        {
            throw new InvalidDataTypeException("Image");
        }
        
        await _minioRepository.DeleteAsync(image.Url, cancellationToken);
        var deletedImage = await _carImageRepository.DeleteAsync(image.Id, cancellationToken);
        await _carImageRepository.SaveChangesAsync(cancellationToken);
        var deletedImageDto = _mapper.Map<ImageDto>(deletedImage);

        return deletedImageDto;
    }
}