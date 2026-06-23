using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TESTEMINHAAPI.Migrations
{
    /// <inheritdoc />
    public partial class rodando1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Licencas_Usuarios_usuario_id",
                table: "Licencas");

            migrationBuilder.AddForeignKey(
                name: "FK_Licencas_Usuarios_usuario_id",
                table: "Licencas",
                column: "usuario_id",
                principalTable: "Usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Licencas_Usuarios_usuario_id",
                table: "Licencas");

            migrationBuilder.AddForeignKey(
                name: "FK_Licencas_Usuarios_usuario_id",
                table: "Licencas",
                column: "usuario_id",
                principalTable: "Usuarios",
                principalColumn: "id");
        }
    }
}
