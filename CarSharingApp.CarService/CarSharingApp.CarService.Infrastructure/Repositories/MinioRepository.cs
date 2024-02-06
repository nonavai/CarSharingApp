using CarSharingApp.CarService.Application.DTO_s.Image;
using CarSharingApp.CarService.Application.Repositories;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel.Args;
using Minio.DataModel.Response;

namespace CarSharingApp.CarService.Infrastructure.Repositories;

public class MinioRepository : IMinioRepository
{
    private readonly MinioClient _minioClient;
    private string _bucketName;

    public MinioRepository(MinioClient minioClient, IConfiguration configuration)
    {
        _minioClient = minioClient;
        _bucketName = configuration["MinIO-Settings:BucketName"];
    }


    public async Task<Stream> GetAsync(string objName, CancellationToken token = default)
    {
        var result = Stream.Null;
        var getObjectArgs = new GetObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(objName)
            .WithCallbackStream((stream =>
                stream.CopyTo(result)));
        await _minioClient.GetObjectAsync(getObjectArgs, token);
        
        return result;
    }

    public async Task<PutObjectResponse?> AddAsync(ImageCleanDto dto, CancellationToken token = default)
    {
        var putObjectArgs = new PutObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(dto.Url)
            .WithStreamData(dto.File)
            .WithObjectSize(dto.File.Length)
            .WithContentType("image/jpeg");
        var result =  await _minioClient.PutObjectAsync(putObjectArgs, token);

        return result;
    }

    public async Task DeleteAsync(string objName, CancellationToken token = default)
    {
        var deleteObjectArgs = new RemoveObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(objName);
        await _minioClient.RemoveObjectAsync(deleteObjectArgs, token);
    }
}