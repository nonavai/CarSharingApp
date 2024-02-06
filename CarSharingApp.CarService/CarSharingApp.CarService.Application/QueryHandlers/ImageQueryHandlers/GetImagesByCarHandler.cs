using CarSharingApp.CarService.Application.DTO_s.Image;
using CarSharingApp.CarService.Application.Queries.ImageQueries;
using CarSharingApp.CarService.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel.Args;

namespace CarSharingApp.CarService.Application.QueryHandlers.ImageQueryHandlers;

public class GetImagesByCarHandler : IRequestHandler<GetImagesByCarQuery, IEnumerable<ImageDto>>
{
    private readonly IMinioRepository _minioRepository;
    private readonly ICarImageRepository _carImageRepository;

    public GetImagesByCarHandler(ICarImageRepository carImageRepository, IMinioRepository minioRepository)
    {
        _carImageRepository = carImageRepository;
        _minioRepository = minioRepository;
    }

    public async Task<IEnumerable<ImageDto>> Handle(GetImagesByCarQuery query, CancellationToken token)
    {
        var carImages = await _carImageRepository.GetByCarIdAsync(query.CarId);
        var imagesTasks = carImages.Select(async image => 
            new ImageDto
            {
                CarId = image.CarId,
                Url = image.Url,
                IsPrimary = image.IsPrimary,
                File = await _minioRepository.GetAsync(image.Url, token)
            });
        var images = await Task.WhenAll(imagesTasks);

        return images.AsEnumerable();
    }
}