using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSharingApp.CarService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCarEntityParameters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "EngineCapacity",
                table: "Cars",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "WheelDrive",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "EngineCapacity",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "WheelDrive",
                table: "Cars");
        }
    }
}
