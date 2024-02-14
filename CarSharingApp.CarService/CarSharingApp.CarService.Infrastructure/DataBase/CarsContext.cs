using CarSharingApp.CarService.Domain.Entities;
using CarSharingApp.CarService.Infrastructure.DataBase.Configurations;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CarSharingApp.CarService.Infrastructure.DataBase;

public class CarsContext : DbContext
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<CarState> CarStates { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<CarImage> CarImages { get; set; }
    
    public CarsContext(DbContextOptions<CarsContext> options, IConfiguration configuration) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarConfiguration).Assembly);
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}