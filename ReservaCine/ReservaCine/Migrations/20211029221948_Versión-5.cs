using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReservaCine.Migrations
{
    public partial class Versión5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcion_Sala_SalaId1",
                table: "Funcion");

            migrationBuilder.DropIndex(
                name: "IX_Funcion_SalaId1",
                table: "Funcion");

            migrationBuilder.DropColumn(
                name: "SalaId1",
                table: "Funcion");

            migrationBuilder.AlterColumn<Guid>(
                name: "SalaId",
                table: "Funcion",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Funcion_SalaId",
                table: "Funcion",
                column: "SalaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcion_Sala_SalaId",
                table: "Funcion",
                column: "SalaId",
                principalTable: "Sala",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcion_Sala_SalaId",
                table: "Funcion");

            migrationBuilder.DropIndex(
                name: "IX_Funcion_SalaId",
                table: "Funcion");

            migrationBuilder.AlterColumn<int>(
                name: "SalaId",
                table: "Funcion",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "SalaId1",
                table: "Funcion",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Funcion_SalaId1",
                table: "Funcion",
                column: "SalaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcion_Sala_SalaId1",
                table: "Funcion",
                column: "SalaId1",
                principalTable: "Sala",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
