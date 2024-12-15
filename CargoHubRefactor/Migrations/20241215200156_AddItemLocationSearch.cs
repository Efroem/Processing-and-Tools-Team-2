using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoHubRefactor.Migrations
{
    /// <inheritdoc />
    public partial class AddItemLocationSearch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Items_ItemId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "OrderItems",
                newName: "ItemUid");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_ItemId",
                table: "OrderItems",
                newName: "IX_OrderItems_ItemUid");

            migrationBuilder.AlterColumn<string>(
                name: "ItemAmountsString",
                table: "Locations",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Locations",
                table: "Inventories",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Items_ItemUid",
                table: "OrderItems",
                column: "ItemUid",
                principalTable: "Items",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Items_ItemUid",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "ItemUid",
                table: "OrderItems",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_ItemUid",
                table: "OrderItems",
                newName: "IX_OrderItems_ItemId");

            migrationBuilder.AlterColumn<string>(
                name: "ItemAmountsString",
                table: "Locations",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Locations",
                table: "Inventories",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Items_ItemId",
                table: "OrderItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
