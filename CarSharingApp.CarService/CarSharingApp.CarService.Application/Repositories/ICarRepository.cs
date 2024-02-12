using CarSharingApp.CarService.Domain.Entities;
using CarSharingApp.CarService.Domain.Specifications;

namespace CarSharingApp.CarService.Application.Repositories;

public interface ICarRepository : IBaseRepository<Car>
{
    public Task<IEnumerable<Car>> GetBySpecAsync(CarSpecification spec, CancellationToken token = default);
    Task<Car?> GetByIdWithInclude(string id, CancellationToken token = default);
}