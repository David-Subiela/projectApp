﻿// <auto-generated />
using System;
using Clase5_proyecto.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Clase5_proyecto.Migrations
{
    [DbContext(typeof(PlayContext))]
    [Migration("20240909134747_InitDB3")]
    partial class InitDB3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Clase5_proyecto.Models.Aeronave", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CapacidadCarga")
                        .HasColumnType("int");

                    b.Property<bool>("Disponibilidad")
                        .HasColumnType("bit");

                    b.Property<int>("HorasVuelo")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Aeronaves");
                });

            modelBuilder.Entity("Clase5_proyecto.Models.MisionEmergencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Destino")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duracion")
                        .HasColumnType("int");

                    b.Property<string>("TipoMision")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("MisionesEmergencia");
                });

            modelBuilder.Entity("Clase5_proyecto.Models.Piloto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Disponibilidad")
                        .HasColumnType("bit");

                    b.Property<int>("HorasVueloAcumuladas")
                        .HasColumnType("int");

                    b.Property<string>("Nombrecompleto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumeroLicencia")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Pilotos");
                });
#pragma warning restore 612, 618
        }
    }
}
