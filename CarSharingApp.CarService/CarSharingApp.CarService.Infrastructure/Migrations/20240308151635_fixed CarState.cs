using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSharingApp.CarService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixedCarState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CarStates");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CarStates",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
