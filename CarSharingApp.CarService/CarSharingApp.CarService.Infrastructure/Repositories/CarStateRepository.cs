using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Domain.Entities;
using CarSharingApp.CarService.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace CarSharingApp.CarService.Infrastructure.Repositories;

public class CarStateRepository : BaseRepository<CarState>, ICarStateRepository
{
    private CarsContext db;

    public CarStateRepository(CarsContext db) : base(db)
    {
        this.db = db;
    }

    public async Task<CarState> GetByCarIdAsync(string id)
    {
        var entity = await db.CarStates.FirstAsync(a => a.CarId == id);
        return entity;
    }
}