using CarSharingApp.CarService.Domain.Entities;
using CarSharingApp.CarService.Infrastructure.DataBase.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CarSharingApp.CarService.Infrastructure.DataBase;

public class CarsContext : DbContext
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<CarState> CarStates { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<CarImage> CarImages { get; set; }

    public string DbPath;
    public CarsContext(DbContextOptions<CarsContext> options, IConfiguration configuration) : base(options)
    {
        DbPath = configuration.GetConnectionString("CarSharingDb");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarConfiguration).Assembly);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(DbPath);
}