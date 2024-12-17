using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoHubRefactor.Migrations
{
    /// <inheritdoc />
    public partial class ReworkedWarehouseLocationsAndItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RestrictedClassifications",
                table: "Warehouses",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RestrictedClassificationsList",
                table: "Warehouses",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<bool>(
                name: "IsDock",
                table: "Locations",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "MaxWeight",
                table: "Locations",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Classification",
                table: "Items",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RestrictedClassifications",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "RestrictedClassificationsList",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "IsDock",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "MaxWeight",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Classification",
                table: "Items");
        }
    }
}
