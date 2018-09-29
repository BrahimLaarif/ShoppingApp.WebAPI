using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingApp.WebAPI.Migrations
{
    public partial class UpdatePhotosTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Photos",
                newName: "FilePath");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Photos",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "Length",
                table: "Photos",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Photos",
                newName: "Url");
        }
    }
}
