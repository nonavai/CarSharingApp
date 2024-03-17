using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Domain.Entities;
using CarSharingApp.CarService.Domain.Specifications;
using CarSharingApp.CarService.Domain.Specifications.SpecSettings;
using CarSharingApp.CarService.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace CarSharingApp.CarService.Infrastructure.Repositories;

public class CarRepository : BaseRepository<Car>, ICarRepository
{
    private CarsContext _dataBase;

    public CarRepository(CarsContext dataBase) : base(dataBase)
    {
        _dataBase = dataBase;
    }

    public async Task<IEnumerable<Car>> GetBySpecAsync(CarSpecification spec, int currentPage, int pageSize,  CancellationToken token = default)
    {
        IQueryable<Car> query = _dataBase.Cars;
        query = query.ApplySpecification(spec)
            .OrderBy(car => car.Price)
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize);
        
        return await query.ToListAsync(cancellationToken: token);
    }

    public async Task<Car?> GetByIdWithIncludeAsync(string id, CancellationToken token = default)
    {
        return await _dataBase.Cars
            .Include(car => car.CarState)
            .FirstOrDefaultAsync(car => car.Id == id, token);
    }

    public async Task<IEnumerable<Car>> GetByUserIdAsync(string id)
    {
        var query = _dataBase.Cars.Where(car => car.UserId == id);
        
        return query.AsEnumerable();
    }
    public async Task DeleteManyAsync(params Car[] entities)
    {
        _dataBase.Cars.RemoveRange(entities);
    }
}