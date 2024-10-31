using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TpFinalLaboratorio.Net.Migrations
{
    /// <inheritdoc />
    public partial class AddImagenToInmueble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Inmuebles",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Inmuebles");
        }
    }
}
