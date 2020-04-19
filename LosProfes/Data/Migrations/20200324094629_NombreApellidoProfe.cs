using Microsoft.EntityFrameworkCore.Migrations;

namespace LosProfes.Data.Migrations
{
    public partial class NombreApellidoProfe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Apellidos",
                table: "Profesor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Profesor",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apellidos",
                table: "Profesor");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Profesor");
        }
    }
}
