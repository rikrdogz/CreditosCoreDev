using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CreditosCore.Migrations
{
    public partial class columnaFechaCredito : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCredito",
                table: "creditos",
                type: "Date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCredito",
                table: "creditos");
        }
    }
}
