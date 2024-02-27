using CarSharingApp.CarService.Domain.Entities;
using CarSharingApp.CarService.Domain.Specifications;

namespace CarSharingApp.CarService.Application.Repositories;

public interface ICarRepository : IBaseRepository<Car>
{
    Task<IEnumerable<Car>> GetBySpecAsync(
        CarSpecification spec,
        int currentPage,
        int pageSize,
        CancellationToken token = default);
    Task<Car?> GetByIdWithIncludeAsync(string id, CancellationToken token = default);
    Task<IEnumerable<Car>> GetByUserIdAsync(string id);
    Task DeleteManyAsync(params Car[] entities);
}