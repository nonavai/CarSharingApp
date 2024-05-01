using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace CarSharingApp.Identity.DataAccess.DbContext;

public class HelperContext : Microsoft.EntityFrameworkCore.DbContext
{
    public HelperContext(DbContextOptions<HelperContext> options) : base(options)
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