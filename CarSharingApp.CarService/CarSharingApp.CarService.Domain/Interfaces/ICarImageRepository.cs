using CarSharingApp.CarService.Domain.Entities;

namespace CarSharingApp.CarService.Domain.Interfaces;

public interface ICarImageRepository : IBaseRepository<CarImage>
{
    Task<IEnumerable<CarImage>> GetByCarIdAsync(string carId);
}