using AutoMapper;
using CarSharingApp.CarService.Application.Caching;
using CarSharingApp.CarService.Application.DTO_s.Image;
using CarSharingApp.CarService.Application.Queries.ImageQueries;
using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Domain.Exceptions;
using MediatR;

namespace CarSharingApp.CarService.Application.QueryHandlers.ImageQueryHandlers;

public class GetPrimaryImageByCarHandler : IRequestHandler<GetPrimaryImageByCarQuery, ImageFullDto>
{
    private readonly IMinioRepository _minioRepository;
    private readonly ICarImageRepository _carImageRepository;
    private readonly IMapper _mapper;

    public GetPrimaryImageByCarHandler(ICarImageRepository carImageRepository, IMinioRepository minioRepository, IMapper mapper)
    {
        _carImageRepository = carImageRepository;
        _minioRepository = minioRepository;
        _mapper = mapper;
    }

    public async Task<ImageFullDto> Handle(GetPrimaryImageByCarQuery query, CancellationToken token)
    {
        var carImage = await _carImageRepository.GetPrimaryAsync(query.CarId, token);
        
        if (carImage == null)
        {
            throw new NotFoundException("Image");
        }
        
        var memoryStream = await _minioRepository.GetAsync(carImage.Url, token);
        var bytes = memoryStream.ToArray();
        var base64String = Convert.ToBase64String(bytes);
        var result = _mapper.Map<ImageFullDto>(carImage);
        result.File = base64String;
        
        return result;
    }
}