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

    public async Task<IEnumerable<Car>> GetBySpecAsync(CarSpecification spec, CancellationToken token = default)
    {
        IQueryable<Car> query = _dataBase.Cars;
        query = query.ApplySpecification(spec);
        
        return await query.ToListAsync(cancellationToken: token);
    }

    public async Task<Car?> GetByIdWithInclude(string id, CancellationToken token = default)
    {
        return await _dataBase.Cars
            .Include(car => car.Comments)
            .Include(car => car.CarState)
            .FirstOrDefaultAsync(p => p.Id == id, token);
    }

    public async Task<IEnumerable<Car>> GetByUserId(string id)
    {
        var query = _dataBase.Cars.Where(car => car.UserId == id);
        
        return query.AsEnumerable();
    }
}