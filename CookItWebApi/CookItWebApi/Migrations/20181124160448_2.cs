using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CookItWebApi.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Perfiles",
                columns: table => new
                {
                    _Email = table.Column<string>(nullable: false),
                    _Foto = table.Column<byte[]>(nullable: true),
                    _NombreUsuario = table.Column<string>(nullable: false),
                    _Nombre = table.Column<string>(nullable: false),
                    _Apellido = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfiles", x => x._Email);
                    table.ForeignKey(
                        name: "FK_Perfiles_Usuarios__Email",
                        column: x => x._Email,
                        principalTable: "Usuarios",
                        principalColumn: "_Email",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Perfiles");
        }
    }
}
