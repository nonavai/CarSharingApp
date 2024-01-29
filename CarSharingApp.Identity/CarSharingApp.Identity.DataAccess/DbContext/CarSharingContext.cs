using CarSharingApp.Identity.DataAccess.DbContext.Configurations;
using CarSharingApp.Identity.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CarSharingApp.Identity.DataAccess.DbContext;

public class CarSharingContext : IdentityDbContext<User>
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserInfo> UserInfos { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }

    private string DbPath = "Server=(localdb)\\mssqllocaldb;Database=CarSharingIdentityDB;Trusted_Connection=True;MultipleActiveResultSets=True;";
    public CarSharingContext()
    {
    }

    public CarSharingContext(DbContextOptions<CarSharingContext> options, IConfiguration configuration) : base(options)
    {
        DbPath = configuration.GetConnectionString("DataBase");
    }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(DbPath);
    }

}