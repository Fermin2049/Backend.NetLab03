using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TpFinalLaboratorio.Net.Migrations
{
    /// <inheritdoc />
    public partial class AddPropietarioIdToInquilino : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PropietarioId",
                table: "Inquilinos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Inquilinos_PropietarioId",
                table: "Inquilinos",
                column: "PropietarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inquilinos_Propietarios_PropietarioId",
                table: "Inquilinos",
                column: "PropietarioId",
                principalTable: "Propietarios",
                principalColumn: "IdPropietario",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inquilinos_Propietarios_PropietarioId",
                table: "Inquilinos");

            migrationBuilder.DropIndex(
                name: "IX_Inquilinos_PropietarioId",
                table: "Inquilinos");

            migrationBuilder.DropColumn(
                name: "PropietarioId",
                table: "Inquilinos");
        }
    }
}
