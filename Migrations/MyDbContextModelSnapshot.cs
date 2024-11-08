﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TpFinalLaboratorio.Net.Data;

#nullable disable

namespace TpFinalLaboratorio.Net.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("TpFinalLaboratorio.Net.Models.Contrato", b =>
                {
                    b.Property<int>("IdContrato")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdContrato"));

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("IdInmueble")
                        .HasColumnType("int");

                    b.Property<int>("IdInquilino")
                        .HasColumnType("int");

                    b.Property<decimal>("MontoAlquiler")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("IdContrato");

                    b.HasIndex("IdInmueble");

                    b.HasIndex("IdInquilino");

                    b.ToTable("Contratos");
                });

            modelBuilder.Entity("TpFinalLaboratorio.Net.Models.Inmueble", b =>
                {
                    b.Property<int>("IdInmueble")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdInmueble"));

                    b.Property<int>("Ambientes")
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("IdPropietario")
                        .HasColumnType("int");

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Uso")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("IdInmueble");

                    b.HasIndex("IdPropietario");

                    b.ToTable("Inmuebles");
                });

            modelBuilder.Entity("TpFinalLaboratorio.Net.Models.Inquilino", b =>
                {
                    b.Property<int>("IdInquilino")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdInquilino"));

                    b.Property<string>("Dni")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DniGarante")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LugarTrabajo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NombreGarante")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("IdInquilino");

                    b.ToTable("Inquilinos");
                });

            modelBuilder.Entity("TpFinalLaboratorio.Net.Models.Pago", b =>
                {
                    b.Property<int>("IdPago")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdPago"));

                    b.Property<DateTime>("FechaPago")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("IdContrato")
                        .HasColumnType("int");

                    b.Property<decimal>("Importe")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("NumeroPago")
                        .HasColumnType("int");

                    b.HasKey("IdPago");

                    b.HasIndex("IdContrato");

                    b.ToTable("Pagos");
                });

            modelBuilder.Entity("TpFinalLaboratorio.Net.Models.Propietario", b =>
                {
                    b.Property<int>("IdPropietario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdPropietario"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Dni")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FotoPerfil")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ResetToken")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ResetTokenExpiry")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("IdPropietario");

                    b.ToTable("Propietarios");
                });

            modelBuilder.Entity("TpFinalLaboratorio.Net.Models.Contrato", b =>
                {
                    b.HasOne("TpFinalLaboratorio.Net.Models.Inmueble", "Inmueble")
                        .WithMany("Contratos")
                        .HasForeignKey("IdInmueble")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TpFinalLaboratorio.Net.Models.Inquilino", "Inquilino")
                        .WithMany("Contratos")
                        .HasForeignKey("IdInquilino")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inmueble");

                    b.Navigation("Inquilino");
                });

            modelBuilder.Entity("TpFinalLaboratorio.Net.Models.Inmueble", b =>
                {
                    b.HasOne("TpFinalLaboratorio.Net.Models.Propietario", "Propietario")
                        .WithMany("Inmuebles")
                        .HasForeignKey("IdPropietario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Propietario");
                });

            modelBuilder.Entity("TpFinalLaboratorio.Net.Models.Pago", b =>
                {
                    b.HasOne("TpFinalLaboratorio.Net.Models.Contrato", "Contrato")
                        .WithMany("Pagos")
                        .HasForeignKey("IdContrato")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contrato");
                });

            modelBuilder.Entity("TpFinalLaboratorio.Net.Models.Contrato", b =>
                {
                    b.Navigation("Pagos");
                });

            modelBuilder.Entity("TpFinalLaboratorio.Net.Models.Inmueble", b =>
                {
                    b.Navigation("Contratos");
                });

            modelBuilder.Entity("TpFinalLaboratorio.Net.Models.Inquilino", b =>
                {
                    b.Navigation("Contratos");
                });

            modelBuilder.Entity("TpFinalLaboratorio.Net.Models.Propietario", b =>
                {
                    b.Navigation("Inmuebles");
                });
#pragma warning restore 612, 618
        }
    }
}
