using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReservaCine.Migrations
{
    public partial class Versión_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pelicula",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FechaLanzamiento = table.Column<DateTime>(nullable: false),
                    Titulo = table.Column<string>(maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(maxLength: 6000, nullable: false),
                    Duracion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pelicula", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoSala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false),
                    Precio = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoSala", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(maxLength: 50, nullable: false),
                    DNI = table.Column<long>(nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Domicilio = table.Column<string>(maxLength: 50, nullable: false),
                    Telefono = table.Column<long>(nullable: false),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    NombreUsuario = table.Column<string>(maxLength: 10, nullable: false),
                    Password = table.Column<byte[]>(maxLength: 15, nullable: false),
                    Rol = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Legajo = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genero",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    PeliculaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Genero_Pelicula_PeliculaId",
                        column: x => x.PeliculaId,
                        principalTable: "Pelicula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Numero = table.Column<int>(nullable: false),
                    tipoSalaId = table.Column<Guid>(nullable: true),
                    CapacidadButacas = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sala_TipoSala_tipoSalaId",
                        column: x => x.tipoSalaId,
                        principalTable: "TipoSala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Funcion",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Hora = table.Column<DateTime>(nullable: false),
                    Descripcion = table.Column<string>(maxLength: 20, nullable: false),
                    CantButacasDisponibles = table.Column<int>(nullable: false),
                    Confirmar = table.Column<bool>(nullable: false),
                    SalaId = table.Column<int>(nullable: false),
                    SalaId1 = table.Column<Guid>(nullable: true),
                    Horario = table.Column<DateTime>(nullable: false),
                    PeliculaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funcion_Pelicula_PeliculaId",
                        column: x => x.PeliculaId,
                        principalTable: "Pelicula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funcion_Sala_SalaId1",
                        column: x => x.SalaId1,
                        principalTable: "Sala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FuncionId = table.Column<Guid>(nullable: true),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    ClienteId = table.Column<Guid>(nullable: false),
                    CantidadButacas = table.Column<int>(nullable: false),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reserva_Usuario_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserva_Funcion_FuncionId",
                        column: x => x.FuncionId,
                        principalTable: "Funcion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Funcion_PeliculaId",
                table: "Funcion",
                column: "PeliculaId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcion_SalaId1",
                table: "Funcion",
                column: "SalaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Genero_PeliculaId",
                table: "Genero",
                column: "PeliculaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_ClienteId",
                table: "Reserva",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_FuncionId",
                table: "Reserva",
                column: "FuncionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sala_tipoSalaId",
                table: "Sala",
                column: "tipoSalaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Genero");

            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Funcion");

            migrationBuilder.DropTable(
                name: "Pelicula");

            migrationBuilder.DropTable(
                name: "Sala");

            migrationBuilder.DropTable(
                name: "TipoSala");
        }
    }
}
