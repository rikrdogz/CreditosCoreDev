using Microsoft.EntityFrameworkCore.Migrations;

namespace CreditosCore.Migrations
{
    public partial class ColumnaPagoObservacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "observacion",
                table: "pagos",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "observacion",
                table: "pagos");
        }
    }
}
