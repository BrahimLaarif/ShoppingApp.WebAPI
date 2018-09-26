using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingApp.WebAPI.Migrations
{
    public partial class RemoveColumnsFromModelsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Models");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Models",
                nullable: false,
                defaultValue: 0);
        }
    }
}
