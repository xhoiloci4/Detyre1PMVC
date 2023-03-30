using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Detyre1PMVC.Data.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategorite",
                columns: table => new
                {
                    KategoriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorite", x => x.KategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Librat",
                columns: table => new
                {
                    LibriId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Autori = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DtPublikimi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NrFaqe = table.Column<int>(type: "int", nullable: false),
                    KategoriId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Librat", x => x.LibriId);
                    table.ForeignKey(
                        name: "FK_Librat_Kategorite_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategorite",
                        principalColumn: "KategoriaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Librat_KategoriId",
                table: "Librat",
                column: "KategoriId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Librat");

            migrationBuilder.DropTable(
                name: "Kategorite");
        }
    }
}
