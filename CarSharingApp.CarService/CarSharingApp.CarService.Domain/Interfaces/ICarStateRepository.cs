using CarSharingApp.CarService.Domain.Entities;

namespace CarSharingApp.CarService.Domain.Interfaces;

public interface ICarStateRepository : IBaseRepository<CarState>
{
    Task<CarState> GetByCarIdAsync(string id);
}