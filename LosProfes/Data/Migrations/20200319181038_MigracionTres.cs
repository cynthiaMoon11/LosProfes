using Microsoft.EntityFrameworkCore.Migrations;

namespace LosProfes.Data.Migrations
{
    public partial class MigracionTres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Profesor_GeneroId",
                table: "Profesor");

            migrationBuilder.CreateIndex(
                name: "IX_Profesor_GeneroId",
                table: "Profesor",
                column: "GeneroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Profesor_GeneroId",
                table: "Profesor");

            migrationBuilder.CreateIndex(
                name: "IX_Profesor_GeneroId",
                table: "Profesor",
                column: "GeneroId",
                unique: true);
        }
    }
}
