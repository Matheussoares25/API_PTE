using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TESTEMINHAAPI.Migrations
{
    /// <inheritdoc />
    public partial class Teste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Treinamentos",
                type: "varchar(125)",
                maxLength: 125,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Treinamentos",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(125)",
                oldMaxLength: 125)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
