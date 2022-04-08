﻿// <auto-generated />
using System;
using CreditosCore.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CreditosCore.Migrations
{
    [DbContext(typeof(SqlDataContext))]
    partial class SqlDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CreditosCore.Controllers.Clientes.ClientesModel", b =>
                {
                    b.Property<int>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApellidoMaterno")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("ApellidoPaterno")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Correo")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Nombre")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ClienteId");

                    b.ToTable("clientes");
                });

            modelBuilder.Entity("CreditosCore.Controllers.Creditos.CreditoAmortizacionPagosModel", b =>
                {
                    b.Property<int>("CreditoAmortizacionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreditoId")
                        .HasColumnType("int");

                    b.Property<int>("EstatusId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaVencimiento")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Monto")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("NumeroPago")
                        .HasColumnType("int");

                    b.HasKey("CreditoAmortizacionId");

                    b.ToTable("creditosamortiguacion");
                });

            modelBuilder.Entity("CreditosCore.Controllers.Creditos.CreditosModel", b =>
                {
                    b.Property<int>("CreditId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AmountDiscountPaymentFinal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Folio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<decimal>("InterestAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("IoanAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("IoanTerm")
                        .HasColumnType("int");

                    b.Property<decimal>("NonPaymentCommission")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PaymentAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TypePayment")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedtionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CreditId");

                    b.ToTable("creditos");
                });
#pragma warning restore 612, 618
        }
    }
}
