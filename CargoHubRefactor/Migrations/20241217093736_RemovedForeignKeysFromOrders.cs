using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoHubRefactor.Migrations
{
    /// <inheritdoc />
    public partial class RemovedForeignKeysFromOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Clients_BillTo",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Clients_ShipTo",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_BillTo",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShipTo",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Orders_BillTo",
                table: "Orders",
                column: "BillTo");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShipTo",
                table: "Orders",
                column: "ShipTo");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Clients_BillTo",
                table: "Orders",
                column: "BillTo",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Clients_ShipTo",
                table: "Orders",
                column: "ShipTo",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
