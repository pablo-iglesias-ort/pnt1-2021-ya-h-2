﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReservaCine.Data;

namespace ReservaCine.Migrations
{
    [DbContext(typeof(ReservaCineContext))]
    partial class ReservaCineContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.18");

            modelBuilder.Entity("ReservaCine.Models.Funcion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("CantButacasDisponibles")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Confirmar")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(20);

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Hora")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PeliculaId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SalaId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PeliculaId");

                    b.HasIndex("SalaId");

                    b.ToTable("Funcion");
                });

            modelBuilder.Entity("ReservaCine.Models.Genero", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Genero");
                });

            modelBuilder.Entity("ReservaCine.Models.Pelicula", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(6000);

                    b.Property<int>("Duracion")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaLanzamiento")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GeneroId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("GeneroId");

                    b.ToTable("Pelicula");
                });

            modelBuilder.Entity("ReservaCine.Models.Reserva", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Activa")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CantidadButacas")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("FuncionId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("FuncionId");

                    b.ToTable("Reserva");
                });

            modelBuilder.Entity("ReservaCine.Models.Sala", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("CapacidadButacas")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Numero")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Sala");
                });

            modelBuilder.Entity("ReservaCine.Models.TipoSala", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(20);

                    b.Property<double>("Precio")
                        .HasColumnType("REAL");

                    b.Property<Guid>("SalaId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SalaId")
                        .IsUnique();

                    b.ToTable("TipoSala");
                });

            modelBuilder.Entity("ReservaCine.Models.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<long>("DNI")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Domicilio")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(10);

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("BLOB")
                        .HasMaxLength(15);

                    b.Property<long>("Telefono")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Usuario");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Usuario");
                });

            modelBuilder.Entity("ReservaCine.Models.Cliente", b =>
                {
                    b.HasBaseType("ReservaCine.Models.Usuario");

                    b.HasDiscriminator().HasValue("Cliente");
                });

            modelBuilder.Entity("ReservaCine.Models.Empleado", b =>
                {
                    b.HasBaseType("ReservaCine.Models.Usuario");

                    b.Property<int>("Legajo")
                        .HasColumnType("INTEGER");

                    b.HasDiscriminator().HasValue("Empleado");
                });

            modelBuilder.Entity("ReservaCine.Models.Funcion", b =>
                {
                    b.HasOne("ReservaCine.Models.Pelicula", "Pelicula")
                        .WithMany("Funciones")
                        .HasForeignKey("PeliculaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReservaCine.Models.Sala", "Sala")
                        .WithMany("Funciones")
                        .HasForeignKey("SalaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReservaCine.Models.Pelicula", b =>
                {
                    b.HasOne("ReservaCine.Models.Genero", "Genero")
                        .WithMany("Peliculas")
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReservaCine.Models.Reserva", b =>
                {
                    b.HasOne("ReservaCine.Models.Cliente", "Cliente")
                        .WithMany("Reservas")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReservaCine.Models.Funcion", "Funcion")
                        .WithMany("Reservas")
                        .HasForeignKey("FuncionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReservaCine.Models.TipoSala", b =>
                {
                    b.HasOne("ReservaCine.Models.Sala", "Sala")
                        .WithOne("tipoSala")
                        .HasForeignKey("ReservaCine.Models.TipoSala", "SalaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
