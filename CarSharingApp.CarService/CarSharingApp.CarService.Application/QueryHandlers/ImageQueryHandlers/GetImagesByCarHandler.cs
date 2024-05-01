using AutoMapper;
using CarSharingApp.CarService.Application.Caching;
using CarSharingApp.CarService.Application.DTO_s.Image;
using CarSharingApp.CarService.Application.Queries.ImageQueries;
using CarSharingApp.CarService.Application.Repositories;
using MediatR;
using Newtonsoft.Json;

namespace CarSharingApp.CarService.Application.QueryHandlers.ImageQueryHandlers;

public class GetImagesByCarHandler : IRequestHandler<GetImagesByCarQuery, IEnumerable<ImageFullDto>>
{
    private readonly IMinioRepository _minioRepository;
    private readonly ICacheService _cacheService;
    private readonly ICarImageRepository _carImageRepository;
    private readonly IMapper _mapper;

    public GetImagesByCarHandler(ICarImageRepository carImageRepository, IMinioRepository minioRepository, ICacheService cacheService, IMapper mapper)
    {
        _carImageRepository = carImageRepository;
        _minioRepository = minioRepository;
        _cacheService = cacheService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ImageFullDto>> Handle(GetImagesByCarQuery query, CancellationToken token)
    {
        var serializedValue = JsonConvert.SerializeObject(query);
        var cache = await _cacheService.GetAsync<IEnumerable<ImageFullDto>>(serializedValue);

        if (cache != null)
        {
            return cache;
        }
        
        var carImages = await _carImageRepository.GetByCarIdAsync(query.CarId);
        var imagesTasks = carImages.Select(async image => 
        {
            var memoryStream = await _minioRepository.GetAsync(image.Url, token);
            var bytes = memoryStream.ToArray();
            var base64String = Convert.ToBase64String(bytes);
            var result = _mapper.Map<ImageFullDto>(image);
            result.File = base64String;
            
            return result;
        });
        var images = await Task.WhenAll(imagesTasks);
        await _cacheService.SetAsync(serializedValue, images);

        return images.AsEnumerable();
    }
}