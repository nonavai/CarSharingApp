using CarSharingApp.CarService.Domain.Entities;
using CarSharingApp.CarService.Domain.Enums;
using CarSharingApp.CarService.Domain.Specifications;
using CarSharingApp.CarService.Domain.Specifications.SpecSettings;

namespace CarSharingApp.CarService.Domain.Interfaces;

public interface ICarRepository : IBaseRepository<Car>
{
    public Task<IEnumerable<Car>> GetBySpecAsync(CarSpecification spec, CancellationToken token);
}