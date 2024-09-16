using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clase5_proyecto.Migrations
{
    /// <inheritdoc />
    public partial class InitDB4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombrecompleto",
                table: "Pilotos",
                newName: "NombreCompleto");

            migrationBuilder.AlterColumn<string>(
                name: "NumeroLicencia",
                table: "Pilotos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AeronaveId",
                table: "MisionesEmergencia",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PilotoId",
                table: "MisionesEmergencia",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MisionesEmergencia_AeronaveId",
                table: "MisionesEmergencia",
                column: "AeronaveId");

            migrationBuilder.CreateIndex(
                name: "IX_MisionesEmergencia_PilotoId",
                table: "MisionesEmergencia",
                column: "PilotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_MisionesEmergencia_Aeronaves_AeronaveId",
                table: "MisionesEmergencia",
                column: "AeronaveId",
                principalTable: "Aeronaves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MisionesEmergencia_Pilotos_PilotoId",
                table: "MisionesEmergencia",
                column: "PilotoId",
                principalTable: "Pilotos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MisionesEmergencia_Aeronaves_AeronaveId",
                table: "MisionesEmergencia");

            migrationBuilder.DropForeignKey(
                name: "FK_MisionesEmergencia_Pilotos_PilotoId",
                table: "MisionesEmergencia");

            migrationBuilder.DropIndex(
                name: "IX_MisionesEmergencia_AeronaveId",
                table: "MisionesEmergencia");

            migrationBuilder.DropIndex(
                name: "IX_MisionesEmergencia_PilotoId",
                table: "MisionesEmergencia");

            migrationBuilder.DropColumn(
                name: "AeronaveId",
                table: "MisionesEmergencia");

            migrationBuilder.DropColumn(
                name: "PilotoId",
                table: "MisionesEmergencia");

            migrationBuilder.RenameColumn(
                name: "NombreCompleto",
                table: "Pilotos",
                newName: "Nombrecompleto");

            migrationBuilder.AlterColumn<int>(
                name: "NumeroLicencia",
                table: "Pilotos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
