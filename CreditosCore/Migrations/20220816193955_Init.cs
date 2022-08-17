using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CreditosCore.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    Correo = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "creditos",
                columns: table => new
                {
                    CreditoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    Folio = table.Column<string>(type: "TEXT", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaCredito = table.Column<DateTime>(type: "Date", nullable: false),
                    MontoPrestamo = table.Column<decimal>(type: "TEXT", nullable: false),
                    MontoInteres = table.Column<decimal>(type: "TEXT", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    MontoPago = table.Column<decimal>(type: "TEXT", nullable: false),
                    Plazos = table.Column<int>(type: "INTEGER", nullable: false),
                    idUsuario = table.Column<int>(type: "INTEGER", nullable: false),
                    DescuentoPagoFinal = table.Column<decimal>(type: "TEXT", nullable: false),
                    TipoPago = table.Column<int>(type: "INTEGER", nullable: false),
                    ComisionFaltaPago = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_creditos", x => x.CreditoId);
                });

            migrationBuilder.CreateTable(
                name: "pagos",
                columns: table => new
                {
                    PagoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreditoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Monto = table.Column<decimal>(type: "TEXT", nullable: false),
                    descuento = table.Column<decimal>(type: "TEXT", nullable: false),
                    faltaDePago = table.Column<decimal>(type: "TEXT", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    fechaPago = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EstatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    idUsuario = table.Column<int>(type: "INTEGER", nullable: false),
                    observacion = table.Column<string>(type: "TEXT", maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pagos", x => x.PagoId);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    usuarioID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nickname = table.Column<string>(type: "TEXT", maxLength: 12, nullable: true),
                    nombre = table.Column<string>(type: "TEXT", maxLength: 25, nullable: true),
                    apellidoPaterno = table.Column<string>(type: "TEXT", maxLength: 40, nullable: true),
                    contra = table.Column<string>(type: "TEXT", nullable: true),
                    ultimoInicioSesion = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.usuarioID);
                });

            migrationBuilder.CreateTable(
                name: "creditosamortiguacion",
                columns: table => new
                {
                    CreditoAmortizacionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumeroPago = table.Column<int>(type: "INTEGER", nullable: false),
                    CreditoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Monto = table.Column<decimal>(type: "TEXT", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EstatusId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_creditosamortiguacion", x => x.CreditoAmortizacionId);
                    table.ForeignKey(
                        name: "FK_creditosamortiguacion_creditos_CreditoId",
                        column: x => x.CreditoId,
                        principalTable: "creditos",
                        principalColumn: "CreditoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_creditosamortiguacion_CreditoId",
                table: "creditosamortiguacion",
                column: "CreditoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.DropTable(
                name: "creditosamortiguacion");

            migrationBuilder.DropTable(
                name: "pagos");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "creditos");
        }
    }
}
