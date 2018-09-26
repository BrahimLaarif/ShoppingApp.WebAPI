using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingApp.WebAPI.Migrations
{
    public partial class AddColumnsToModelsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Models",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Models",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Models");
        }
    }
}
