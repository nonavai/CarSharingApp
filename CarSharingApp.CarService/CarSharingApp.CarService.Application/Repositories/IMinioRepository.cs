using CarSharingApp.CarService.Application.DTO_s.Image;
using Minio.DataModel.Response;

namespace CarSharingApp.CarService.Application.Repositories;

public interface IMinioRepository
{
    Task<Stream> GetAsync(string objName, CancellationToken token = default);
    Task<PutObjectResponse?> AddAsync(ImageCleanDto dto, CancellationToken token = default);
    Task DeleteAsync(string objName, CancellationToken token = default);
}