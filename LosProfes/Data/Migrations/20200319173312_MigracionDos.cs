using Microsoft.EntityFrameworkCore.Migrations;

namespace LosProfes.Data.Migrations
{
    public partial class MigracionDos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "Profesor");

            migrationBuilder.AddColumn<int>(
                name: "GeneroId",
                table: "Profesor",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Genero",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profesor_GeneroId",
                table: "Profesor",
                column: "GeneroId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Profesor_Genero_GeneroId",
                table: "Profesor",
                column: "GeneroId",
                principalTable: "Genero",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profesor_Genero_GeneroId",
                table: "Profesor");

            migrationBuilder.DropTable(
                name: "Genero");

            migrationBuilder.DropIndex(
                name: "IX_Profesor_GeneroId",
                table: "Profesor");

            migrationBuilder.DropColumn(
                name: "GeneroId",
                table: "Profesor");

            migrationBuilder.AddColumn<string>(
                name: "Sexo",
                table: "Profesor",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
