using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Domain.Entities;
using CarSharingApp.CarService.Domain.Specifications;
using CarSharingApp.CarService.Domain.Specifications.SpecSettings;
using CarSharingApp.CarService.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace CarSharingApp.CarService.Infrastructure.Repositories;

public class CarRepository : BaseRepository<Car>, ICarRepository
{
    private CarsContext db;

    public CarRepository(CarsContext db) : base(db)
    {
        this.db = db;
    }

    public async Task<IEnumerable<Car>> GetBySpecAsync(CarSpecification spec, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        IQueryable<Car> query = db.Cars;
        query = query.ApplySpecification(spec);
        return await query.ToListAsync();
    }
}