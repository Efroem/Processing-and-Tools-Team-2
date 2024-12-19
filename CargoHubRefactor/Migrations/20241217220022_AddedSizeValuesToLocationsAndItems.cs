using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoHubRefactor.Migrations
{
    /// <inheritdoc />
    public partial class AddedSizeValuesToLocationsAndItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MaxDepth",
                table: "Locations",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MaxHeight",
                table: "Locations",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MaxWidth",
                table: "Locations",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Depth",
                table: "Items",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "Items",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Width",
                table: "Items",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxDepth",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "MaxHeight",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "MaxWidth",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Depth",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Items");
        }
    }
}
