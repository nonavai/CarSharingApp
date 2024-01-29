using CarSharingApp.CarService.Domain.Entities;
using CarSharingApp.CarService.Domain.Interfaces;
using CarSharingApp.CarService.Infrastructure.DataBase;

namespace CarSharingApp.CarService.Infrastructure.Repositories;

public class PictureRepository: BaseRepository<Picture>, IPictureRepository
{
    private CarsContext db;
    
    public PictureRepository(CarsContext db) : base(db)
    {
        this.db = db;
    }
}