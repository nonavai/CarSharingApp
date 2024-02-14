﻿using CarSharingApp.CarService.Domain.Entities;
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
            .HasMany(c => c.CarImage)
            .WithOne(ci => ci.Car)
            .HasForeignKey(ci => ci.CarId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .HasOne(c => c.CarState)
            .WithOne(cs => cs.Car)
            .HasForeignKey<CarState>(cs => cs.CarId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Property(car => car.Color)
            .HasColumnType("nvarchar(20)");
        
        modelBuilder
            .Property(car => car.Description)
            .HasColumnType("nvarchar(500)");
        
        modelBuilder
            .Property(car => car.Mark)
            .HasColumnType("nvarchar(30)");
        
        modelBuilder
            .Property(car => car.Model)
            .HasColumnType("nvarchar(30)");
        
        modelBuilder
            .Property(car => car.VehicleBody)
            .HasColumnType("nvarchar(17)");
        
        modelBuilder
            .Property(car => car.Mark)
            .HasColumnType("nvarchar(20)");
    }
}