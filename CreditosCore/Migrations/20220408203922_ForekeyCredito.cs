using Microsoft.EntityFrameworkCore.Migrations;

namespace CreditosCore.Migrations
{
    public partial class ForekeyCredito : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedtionDate",
                table: "creditos",
                newName: "FechaModificacion");

            migrationBuilder.RenameColumn(
                name: "TypePayment",
                table: "creditos",
                newName: "idUsuario");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "creditos",
                newName: "MontoTotal");

            migrationBuilder.RenameColumn(
                name: "PaymentAmount",
                table: "creditos",
                newName: "MontoPrestamo");

            migrationBuilder.RenameColumn(
                name: "NonPaymentCommission",
                table: "creditos",
                newName: "MontoPago");

            migrationBuilder.RenameColumn(
                name: "IoanTerm",
                table: "creditos",
                newName: "TipoPago");

            migrationBuilder.RenameColumn(
                name: "IoanAmount",
                table: "creditos",
                newName: "MontoInteres");

            migrationBuilder.RenameColumn(
                name: "InterestAmount",
                table: "creditos",
                newName: "DescuentoPagoFinal");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "creditos",
                newName: "Plazos");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "creditos",
                newName: "FechaCreacion");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "creditos",
                newName: "ClienteId");

            migrationBuilder.RenameColumn(
                name: "AmountDiscountPaymentFinal",
                table: "creditos",
                newName: "ComisionFaltaPago");

            migrationBuilder.RenameColumn(
                name: "CreditId",
                table: "creditos",
                newName: "CreditoId");

            migrationBuilder.AlterColumn<string>(
                name: "Folio",
                table: "creditos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "clientes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApellidoPaterno",
                table: "clientes",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApellidoMaterno",
                table: "clientes",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_creditosamortiguacion_CreditoId",
                table: "creditosamortiguacion",
                column: "CreditoId");

            migrationBuilder.AddForeignKey(
                name: "FK_creditosamortiguacion_creditos_CreditoId",
                table: "creditosamortiguacion",
                column: "CreditoId",
                principalTable: "creditos",
                principalColumn: "CreditoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_creditosamortiguacion_creditos_CreditoId",
                table: "creditosamortiguacion");

            migrationBuilder.DropIndex(
                name: "IX_creditosamortiguacion_CreditoId",
                table: "creditosamortiguacion");

            migrationBuilder.RenameColumn(
                name: "idUsuario",
                table: "creditos",
                newName: "TypePayment");

            migrationBuilder.RenameColumn(
                name: "TipoPago",
                table: "creditos",
                newName: "IoanTerm");

            migrationBuilder.RenameColumn(
                name: "Plazos",
                table: "creditos",
                newName: "IdUser");

            migrationBuilder.RenameColumn(
                name: "MontoTotal",
                table: "creditos",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "MontoPrestamo",
                table: "creditos",
                newName: "PaymentAmount");

            migrationBuilder.RenameColumn(
                name: "MontoPago",
                table: "creditos",
                newName: "NonPaymentCommission");

            migrationBuilder.RenameColumn(
                name: "MontoInteres",
                table: "creditos",
                newName: "IoanAmount");

            migrationBuilder.RenameColumn(
                name: "FechaModificacion",
                table: "creditos",
                newName: "UpdatedtionDate");

            migrationBuilder.RenameColumn(
                name: "FechaCreacion",
                table: "creditos",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "DescuentoPagoFinal",
                table: "creditos",
                newName: "InterestAmount");

            migrationBuilder.RenameColumn(
                name: "ComisionFaltaPago",
                table: "creditos",
                newName: "AmountDiscountPaymentFinal");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "creditos",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "CreditoId",
                table: "creditos",
                newName: "CreditId");

            migrationBuilder.AlterColumn<string>(
                name: "Folio",
                table: "creditos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "clientes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ApellidoPaterno",
                table: "clientes",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "ApellidoMaterno",
                table: "clientes",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);
        }
    }
}
