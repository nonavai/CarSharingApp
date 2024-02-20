using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CarSharingApp.DealService.DataAccess.DataBase.AdditionalDB;

public class HelperContext : DbContext
{
    public HelperContext()
    { 
        
    }
    public HelperContext(DbContextOptions<HelperContext> options, IConfiguration configuration) : base(options)
    {
        DbPath = configuration.GetConnectionString("HelperDB");
    }

    public string DbPath;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}