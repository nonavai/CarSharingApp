using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Domain.Entities;
using CarSharingApp.CarService.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace CarSharingApp.CarService.Infrastructure.Repositories;

public class CarStateRepository : BaseRepository<CarState>, ICarStateRepository
{
    private CarsContext _dataBase;

    public CarStateRepository(CarsContext dataBase) : base(dataBase)
    {
        _dataBase = dataBase;
    }

    public async Task<CarState> GetByCarIdAsync(string id, CancellationToken token)
    {
        var entity = await _dataBase.CarStates.FirstAsync(a => a.CarId == id, cancellationToken: token);
        
        return entity;
    }
}