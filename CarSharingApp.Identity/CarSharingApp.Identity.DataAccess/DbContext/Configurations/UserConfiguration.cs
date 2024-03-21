using CarSharingApp.Identity.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarSharingApp.Identity.DataAccess.DbContext.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> modelBuilder)
    {
        modelBuilder
            .HasOne(user => user.UserInfo)
            .WithOne(userInfo => userInfo.User)
            .HasForeignKey<UserInfo>(userInfo => userInfo.UserId);

        modelBuilder
            .Property(user => user.RecordNumber)
            .HasColumnType("nvarchar(30)");
    }
}