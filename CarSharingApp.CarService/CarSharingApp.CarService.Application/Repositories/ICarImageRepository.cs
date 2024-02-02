using CarSharingApp.CarService.Domain.Entities;

namespace CarSharingApp.CarService.Application.Repositories;

public interface ICarImageRepository : IBaseRepository<CarImage>
{
    Task<IEnumerable<CarImage>> GetByCarIdAsync(string carId);
    Task<CarImage?> GetPrimaryAsync();
}