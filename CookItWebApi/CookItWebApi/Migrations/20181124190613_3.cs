using Microsoft.EntityFrameworkCore.Migrations;

namespace CookItWebApi.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IngredientesUsuarios",
                columns: table => new
                {
                    _IdIngrediente = table.Column<int>(nullable: false),
                    _Cantidad = table.Column<int>(nullable: false),
                    _Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientesUsuarios", x => new { x._IdIngrediente, x._Email });
                    table.ForeignKey(
                        name: "FK_IngredientesUsuarios_Usuarios__Email",
                        column: x => x._Email,
                        principalTable: "Usuarios",
                        principalColumn: "_Email",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientesUsuarios_Ingredientes__IdIngrediente",
                        column: x => x._IdIngrediente,
                        principalTable: "Ingredientes",
                        principalColumn: "_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientesUsuarios__Email",
                table: "IngredientesUsuarios",
                column: "_Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientesUsuarios");
        }
    }
}
