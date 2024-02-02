using CarSharingApp.CarService.Application.DTO_s.Image;
using CarSharingApp.CarService.Application.Queries.ImageQueries;
using CarSharingApp.CarService.Application.Repositories;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel.Args;

namespace CarSharingApp.CarService.Application.QueryHandlers.ImageQueryHandlers;

public class GetImagesByCarHandler : IQueryHandler<GetImagesByCarQuery, IEnumerable<ImageDto>>
{
    private readonly MinioClient _minioClient;
    private readonly ICarImageRepository _carImageRepository;
    private string _bucketName;

    public GetImagesByCarHandler(MinioClient minioClient, IConfiguration configuration, ICarImageRepository carImageRepository)
    {
        _minioClient = minioClient;
        _carImageRepository = carImageRepository;
        _bucketName = configuration["MinIO-Settings:BucketName"];
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
                File = await GetImageAsync(image.Url)
            });
        var images = await Task.WhenAll(imagesTasks);

        return images.AsEnumerable();
    }
    
    private async Task<Stream> GetImageAsync(string objectName)
    {
        var result = Stream.Null;
        var getObjectArgs = new GetObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(objectName)
            .WithCallbackStream((stream =>
                stream.CopyTo(result)));
        
        await _minioClient.GetObjectAsync(getObjectArgs);
        
        return result;
    }
}