﻿// <auto-generated />
using System;
using ControlVacacionesBackEnd.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ControlVacacionesBackEnd.Migrations
{
    [DbContext(typeof(ControlVacacionesBackEndContext))]
    [Migration("20230815205022_FuncionesEscalares")]
    partial class FuncionesEscalares
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ControlVacacionesBackEnd.Models.Empleado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaIngreso")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdTipoIdentificacion")
                        .HasColumnType("int");

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroIdentificacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SalarioBaseMensual")
                        .HasColumnType("decimal(18,2");

                    b.HasKey("Id");

                    b.ToTable("Empleado");
                });

            modelBuilder.Entity("ControlVacacionesBackEnd.Models.TiposIdentificacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("EmpleadoId")
                        .HasColumnType("int");

                    b.Property<string>("TipoIdentificacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmpleadoId");

                    b.ToTable("TiposIdentificacion");
                });

            modelBuilder.Entity("ControlVacacionesBackEnd.Models.TiposIdentificacion", b =>
                {
                    b.HasOne("ControlVacacionesBackEnd.Models.Empleado", null)
                        .WithMany("TiposIdentificaciones")
                        .HasForeignKey("EmpleadoId");
                });

            modelBuilder.Entity("ControlVacacionesBackEnd.Models.Empleado", b =>
                {
                    b.Navigation("TiposIdentificaciones");
                });
#pragma warning restore 612, 618
        }
    }
}