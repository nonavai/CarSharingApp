using CarSharingApp.CarService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarSharingApp.CarService.Infrastructure.DataBase.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> modelBuilder)
    {
        modelBuilder
            .Property(comment => comment.Text)
            .HasColumnType("nvarchar(500)");
    }
}