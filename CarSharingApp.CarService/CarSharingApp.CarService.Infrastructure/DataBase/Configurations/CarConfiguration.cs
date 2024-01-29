using CarSharingApp.CarService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarSharingApp.CarService.Infrastructure.DataBase.Configurations;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> modelBuilder)
    {
        modelBuilder
            .HasMany(c => c.Comments)
            .WithOne(com => com.Car)
            .HasForeignKey(com=> com.CarId)
            .OnDelete(DeleteBehavior.Cascade);
            
        modelBuilder
            .HasMany(c => c.Pictures)
            .WithOne(p => p.Car)
            .HasForeignKey(p=> p.CarId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder
            .HasOne(c => c.CarState)
            .WithOne(c => c.Car)
            .HasForeignKey<CarState>(c => c.CarId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}