using CarSharingApp.CarService.Domain.Entities;
using CarSharingApp.CarService.Infrastructure.DataBase.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CarSharingApp.CarService.Infrastructure.DataBase;

public class CarsContext : DbContext
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<CarState> CarStates { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<CarImage> CarImages { get; set; }

    public CarsContext(DbContextOptions<CarsContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CarConfiguration());
    }
}