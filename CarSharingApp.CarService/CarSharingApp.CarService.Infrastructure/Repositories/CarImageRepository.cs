using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Domain.Entities;
using CarSharingApp.CarService.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace CarSharingApp.CarService.Infrastructure.Repositories;

public class CarImageRepository : BaseRepository<CarImage>, ICarImageRepository
{
    private readonly CarsContext db;
    
    public CarImageRepository(CarsContext db) : base(db)
    {
        this.db = db;
    }

    public async Task<IEnumerable<CarImage>> GetByCarIdAsync(string carId)
    {
        var carImages = db.CarImages.AsNoTracking().AsQueryable().Where(image => image.CarId == carId);
        
        return carImages.AsEnumerable();
    }

    public async Task<CarImage?> GetPrimaryAsync()
    {
        var entity = await db.CarImages.FirstOrDefaultAsync(image => image.IsPrimary);

        return entity;
    }
}