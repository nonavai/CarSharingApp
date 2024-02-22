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

    public GetImagesByCarHandler(ICarImageRepository carImageRepository, IMinioRepository minioRepository, ICacheService cacheService)
    {
        _carImageRepository = carImageRepository;
        _minioRepository = minioRepository;
        _cacheService = cacheService;
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

            return new ImageFullDto
            {
                CarId = image.CarId,
                Url = image.Url,
                IsPrimary = image.IsPrimary,
                File = base64String
            };
        });
        var images = await Task.WhenAll(imagesTasks);
        await _cacheService.SetAsync(serializedValue, images);

        return images.AsEnumerable();
    }
}