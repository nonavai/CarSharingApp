using CarSharingApp.Identity.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarSharingApp.Identity.DataAccess.DbContext.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> modelBuilder)
    {
        modelBuilder
            .HasOne(u => u.UserInfo)
            .WithOne(ui => ui.User)
            .HasForeignKey<UserInfo>(ui => ui.UserId);

    }
}