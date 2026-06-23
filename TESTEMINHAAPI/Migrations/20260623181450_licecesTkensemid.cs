using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TESTEMINHAAPI.Migrations
{
    /// <inheritdoc />
    public partial class licecesTkensemid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Licencas_Usuarios_usuarioid",
                table: "Licencas");

            migrationBuilder.DropIndex(
                name: "IX_Licencas_usuarioid",
                table: "Licencas");

            migrationBuilder.DropColumn(
                name: "usuarioid",
                table: "Licencas");

            migrationBuilder.CreateIndex(
                name: "IX_Licencas_usuario_id",
                table: "Licencas",
                column: "usuario_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Licencas_Usuarios_usuario_id",
                table: "Licencas",
                column: "usuario_id",
                principalTable: "Usuarios",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Licencas_Usuarios_usuario_id",
                table: "Licencas");

            migrationBuilder.DropIndex(
                name: "IX_Licencas_usuario_id",
                table: "Licencas");

            migrationBuilder.AddColumn<int>(
                name: "usuarioid",
                table: "Licencas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Licencas_usuarioid",
                table: "Licencas",
                column: "usuarioid");

            migrationBuilder.AddForeignKey(
                name: "FK_Licencas_Usuarios_usuarioid",
                table: "Licencas",
                column: "usuarioid",
                principalTable: "Usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
