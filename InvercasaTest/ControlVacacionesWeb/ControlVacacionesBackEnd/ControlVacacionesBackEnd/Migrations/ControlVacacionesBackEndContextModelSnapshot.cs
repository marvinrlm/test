﻿// <auto-generated />
using System;
using ControlVacacionesBackEnd.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ControlVacacionesBackEnd.Migrations
{
    [DbContext(typeof(ControlVacacionesBackEndContext))]
    partial class ControlVacacionesBackEndContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Empleado");
                });

            modelBuilder.Entity("ControlVacacionesBackEnd.Models.EstadoVacacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EstadosVacacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EstadoVacacion");
                });

            modelBuilder.Entity("ControlVacacionesBackEnd.Models.RegistroVacacion", b =>
                {
                    b.Property<int>("IdEmpleado")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("FechaVacacion")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(1);

                    b.Property<int>("IdEstadoVacacion")
                        .HasColumnType("int");

                    b.HasKey("IdEmpleado", "FechaVacacion");

                    b.ToTable("RegistroVacacion");
                });

            modelBuilder.Entity("ControlVacacionesBackEnd.Models.TablasSinLlaves.VistaEmpleados", b =>
                {
                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaIngreso")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("IdTipoIdentificacion")
                        .HasColumnType("int");

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroIdentificacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SalarioBaseMensual")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TipoIdentificacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("VistaEmpleados");
                });

            modelBuilder.Entity("ControlVacacionesBackEnd.Models.TiposIdentificacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("TipoIdentificacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TiposIdentificacion");
                });
#pragma warning restore 612, 618
        }
    }
}
