using CarSharingApp.CarService.Domain.Entities;

namespace CarSharingApp.CarService.Application.Repositories;

public interface ICarStateRepository : IBaseRepository<CarState>
{
    Task<CarState> GetByCarIdAsync(string id);
}