using CarSharingApp.Identity.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarSharingApp.Identity.DataAccess.DbContext.Configurations;

public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
{
    public void Configure(EntityTypeBuilder<UserInfo> modelBuilder)
    {
        modelBuilder
            .Property(userInfo => userInfo.LicenceId)
            .HasColumnType("nvarchar(20)");
        
        modelBuilder
            .Property(userInfo => userInfo.PlaceOfIssue)
            .HasColumnType("nvarchar(50)");
    }
}