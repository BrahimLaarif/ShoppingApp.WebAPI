using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingApp.WebAPI.Migrations
{
    public partial class LinkProductsTableToModelsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Models",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Models_ProductId",
                table: "Models",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Products_ProductId",
                table: "Models",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Products_ProductId",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_Models_ProductId",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Models");
        }
    }
}
