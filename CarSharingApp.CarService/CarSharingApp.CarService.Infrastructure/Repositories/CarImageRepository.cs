using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Domain.Entities;
using CarSharingApp.CarService.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace CarSharingApp.CarService.Infrastructure.Repositories;

public class CarImageRepository : BaseRepository<CarImage>, ICarImageRepository
{
    private readonly CarsContext _dataBase;
    
    public CarImageRepository(CarsContext dataBase) : base(dataBase)
    {
        _dataBase = dataBase;
    }

    public async Task<IEnumerable<CarImage>> GetByCarIdAsync(string carId)
    {
        var carImages = _dataBase.CarImages.AsNoTracking().AsQueryable().Where(image => image.CarId == carId);
        
        return carImages.AsEnumerable();
    }

    public async Task<CarImage?> GetPrimaryAsync(string carId, CancellationToken token = default)
    {
        var entity = await _dataBase.CarImages.FirstOrDefaultAsync(image => image.IsPrimary && image.CarId == carId, cancellationToken: token);

        return entity;
    }
}