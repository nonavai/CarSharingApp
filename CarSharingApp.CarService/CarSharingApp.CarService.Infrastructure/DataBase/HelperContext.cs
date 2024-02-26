using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CarSharingApp.CarService.Infrastructure.DataBase;

public class HelperContext : DbContext
{
    public HelperContext(DbContextOptions<HelperContext> options, IConfiguration configuration) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}