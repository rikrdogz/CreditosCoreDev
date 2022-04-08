using Microsoft.EntityFrameworkCore.Migrations;

namespace CreditosCore.Migrations
{
    public partial class CambiarClientesTabla : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Clientes",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Clientes",
                newName: "ApellidoPaterno");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Clientes",
                newName: "ApellidoMaterno");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Clientes",
                newName: "Correo");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Clientes",
                newName: "ClienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Clientes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Correo",
                table: "Clientes",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "ApellidoPaterno",
                table: "Clientes",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "ApellidoMaterno",
                table: "Clientes",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Clientes",
                newName: "ClientId");
        }
    }
}
