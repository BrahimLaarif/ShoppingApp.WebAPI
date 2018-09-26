using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingApp.WebAPI.Migrations
{
    public partial class AddModelSizesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ModelSizes",
                columns: table => new
                {
                    ModelId = table.Column<int>(nullable: false),
                    SizeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelSizes", x => new { x.ModelId, x.SizeId });
                    table.ForeignKey(
                        name: "FK_ModelSizes_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModelSizes_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModelSizes_SizeId",
                table: "ModelSizes",
                column: "SizeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModelSizes");
        }
    }
}
