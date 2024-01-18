using CarSharingApp.Identity.DataAccess.DbContext.Configurations;
using CarSharingApp.Identity.DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarSharingApp.Identity.DataAccess.DbContext;

public class CarSharingContext : IdentityDbContext<User>
{
    
    public DbSet<UserInfo> UserInfos { get; set; }

    public CarSharingContext(DbContextOptions<CarSharingContext> options) : base(options)
    {
    }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
    
}