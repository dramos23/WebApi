using Microsoft.EntityFrameworkCore.Migrations;

namespace CookItWebApi.Migrations
{
    public partial class _7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_PasoRecetas__IdPasoReceta",
                table: "PasoRecetas",
                column: "_IdPasoReceta");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_PasoRecetas__IdPasoReceta",
                table: "PasoRecetas");
        }
    }
}
