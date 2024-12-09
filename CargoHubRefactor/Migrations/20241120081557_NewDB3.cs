using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoHubRefactor.Migrations
{
    /// <inheritdoc />
    public partial class NewDB3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Suppliers_SupplierId",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "ItemLine",
                table: "ItemTypes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ItemGroup",
                table: "ItemLines",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ItemTypes_ItemLine",
                table: "ItemTypes",
                column: "ItemLine");

            migrationBuilder.CreateIndex(
                name: "IX_ItemLines_ItemGroup",
                table: "ItemLines",
                column: "ItemGroup");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemLines_ItemGroups_ItemGroup",
                table: "ItemLines",
                column: "ItemGroup",
                principalTable: "ItemGroups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemTypes_ItemLines_ItemLine",
                table: "ItemTypes",
                column: "ItemLine",
                principalTable: "ItemLines",
                principalColumn: "LineId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Suppliers_SupplierId",
                table: "Items",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "SupplierId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemLines_ItemGroups_ItemGroup",
                table: "ItemLines");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemTypes_ItemLines_ItemLine",
                table: "ItemTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Suppliers_SupplierId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_ItemTypes_ItemLine",
                table: "ItemTypes");

            migrationBuilder.DropIndex(
                name: "IX_ItemLines_ItemGroup",
                table: "ItemLines");

            migrationBuilder.DropColumn(
                name: "ItemLine",
                table: "ItemTypes");

            migrationBuilder.DropColumn(
                name: "ItemGroup",
                table: "ItemLines");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Suppliers_SupplierId",
                table: "Items",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "SupplierId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
