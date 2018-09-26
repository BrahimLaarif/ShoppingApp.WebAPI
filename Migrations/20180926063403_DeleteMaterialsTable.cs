using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingApp.WebAPI.Migrations
{
    public partial class DeleteMaterialsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Materials_MaterialId",
                table: "Models");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Models_MaterialId",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "Models");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaterialId",
                table: "Models",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Models_MaterialId",
                table: "Models",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Materials_MaterialId",
                table: "Models",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
