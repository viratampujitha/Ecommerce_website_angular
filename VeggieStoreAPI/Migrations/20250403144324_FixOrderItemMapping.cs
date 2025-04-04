using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeggieStoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixOrderItemMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Vegetables_VegetableId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "OrderItems",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "VegetableId",
                table: "OrderItems",
                newName: "vegetable_id");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderItems",
                newName: "order_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_VegetableId",
                table: "OrderItems",
                newName: "IX_OrderItems_vegetable_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                newName: "IX_OrderItems_order_id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_order_id",
                table: "OrderItems",
                column: "order_id",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Vegetables_vegetable_id",
                table: "OrderItems",
                column: "vegetable_id",
                principalTable: "Vegetables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_order_id",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Vegetables_vegetable_id",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "OrderItems",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "vegetable_id",
                table: "OrderItems",
                newName: "VegetableId");

            migrationBuilder.RenameColumn(
                name: "order_id",
                table: "OrderItems",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_vegetable_id",
                table: "OrderItems",
                newName: "IX_OrderItems_VegetableId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_order_id",
                table: "OrderItems",
                newName: "IX_OrderItems_OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Vegetables_VegetableId",
                table: "OrderItems",
                column: "VegetableId",
                principalTable: "Vegetables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
