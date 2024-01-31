using CarSharingApp.CarService.Application.DTO_s.Image;
using CarSharingApp.CarService.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel.Args;

namespace CarSharingApp.CarService.Application.Services.Implementations;

public class ImageService : IImageService
{
    private readonly MinioClient _minioClient;
    private readonly IConfiguration _configuration;
    private readonly ICarImageRepository _carImageRepository;
    private string _bucketName;

    public ImageService(MinioClient minioClient, IConfiguration configuration, ICarImageRepository carImageRepository)
    {
        _minioClient = minioClient;
        _configuration = configuration;
        _carImageRepository = carImageRepository;
        _bucketName = _configuration["MinIO-Settings:BucketName"];
        InitializeBucket().Wait();
    }

    public async Task<string> UploadImageAsync(Stream imageStream, string objectName)
    {
        var putObjectArgs = new PutObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(objectName)
            .WithStreamData(imageStream)
            .WithObjectSize(imageStream.Length)
            .WithContentType("image/jpeg");
        
        var result =  await _minioClient.PutObjectAsync(putObjectArgs);

        return result.ObjectName;
    }

    public async Task<IEnumerable<ImageDto>> GetImagesByCarAsync(string carId)
    {
        var carImages = await _carImageRepository.GetByCarIdAsync(carId);
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

    private async Task InitializeBucket()
    {
        var bucketExistsArgs = new BucketExistsArgs().WithBucket(_bucketName);
        var isExist = await _minioClient.BucketExistsAsync(bucketExistsArgs);
        
        if (!isExist)
        {
            var makeBucketArgs = new MakeBucketArgs().WithBucket(_bucketName);
            await _minioClient.MakeBucketAsync(makeBucketArgs);
        }
    }
}