using CarSharingApp.CarService.Application.Queries.ImageQueries;
using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Application.Responses.Image;
using MediatR;

namespace CarSharingApp.CarService.Application.QueryHandlers.ImageQueryHandlers;

public class GetImagesByCarHandler : IRequestHandler<GetImagesByCarQuery, IEnumerable<ImageQueryResponse>>
{
    private readonly IMinioRepository _minioRepository;
    private readonly ICarImageRepository _carImageRepository;

    public GetImagesByCarHandler(ICarImageRepository carImageRepository, IMinioRepository minioRepository)
    {
        _carImageRepository = carImageRepository;
        _minioRepository = minioRepository;
    }

    public async Task<IEnumerable<ImageQueryResponse>> Handle(GetImagesByCarQuery query, CancellationToken token)
    {
        var carImages = await _carImageRepository.GetByCarIdAsync(query.CarId);
        var imagesTasks = carImages.Select(async image => 
        {
            var memoryStream = await _minioRepository.GetAsync(image.Url, token);
            var bytes = memoryStream.ToArray();
            var base64String = Convert.ToBase64String(bytes);

            return new ImageQueryResponse
            {
                CarId = image.CarId,
                Url = image.Url,
                IsPrimary = image.IsPrimary,
                File = base64String
            };
        });
        var images = await Task.WhenAll(imagesTasks);

        return images.AsEnumerable();
    }
}