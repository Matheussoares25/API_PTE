using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TESTEMINHAAPI.Migrations
{
    /// <inheritdoc />
    public partial class requests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aulas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(125)", maxLength: 125, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Conteudo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModuloId = table.Column<int>(type: "int", nullable: false),
                    Criado = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aulas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aulas_Modulos_ModuloId",
                        column: x => x.ModuloId,
                        principalTable: "Modulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Certificados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    TreinamentoId = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmitidoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificados_Treinamentos_TreinamentoId",
                        column: x => x.TreinamentoId,
                        principalTable: "Treinamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Certificados_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Notas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    ProvaId = table.Column<int>(type: "int", nullable: true),
                    TreinamentoId = table.Column<int>(type: "int", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Criado = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Mensagem = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Criado = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UseProva",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    ProvaId = table.Column<int>(type: "int", nullable: false),
                    Nota = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    RealizadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UseProva", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UseProva_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UseTreinamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    TreinamentoId = table.Column<int>(type: "int", nullable: false),
                    MatriculadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UseTreinamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UseTreinamentos_Treinamentos_TreinamentoId",
                        column: x => x.TreinamentoId,
                        principalTable: "Treinamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UseTreinamentos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Vagas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Localizacao = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Ativa = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Criado = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vagas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Midias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AulaId = table.Column<int>(type: "int", nullable: false),
                    Criado = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Midias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Midias_Aulas_AulaId",
                        column: x => x.AulaId,
                        principalTable: "Aulas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Progress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    AulaId = table.Column<int>(type: "int", nullable: false),
                    Percentual = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TempoSegundos = table.Column<int>(type: "int", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Progress_Aulas_AulaId",
                        column: x => x.AulaId,
                        principalTable: "Aulas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Progress_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Questoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Texto = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AulaId = table.Column<int>(type: "int", nullable: false),
                    Criado = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questoes_Aulas_AulaId",
                        column: x => x.AulaId,
                        principalTable: "Aulas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Candidaturas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VagaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    Nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CurriculoUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Criado = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidaturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidaturas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Candidaturas_Vagas_VagaId",
                        column: x => x.VagaId,
                        principalTable: "Vagas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Alternativas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuestaoId = table.Column<int>(type: "int", nullable: false),
                    Texto = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Correta = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Ordem = table.Column<int>(type: "int", nullable: false),
                    Criado = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alternativas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alternativas_Questoes_QuestaoId",
                        column: x => x.QuestaoId,
                        principalTable: "Questoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Alternativas_QuestaoId",
                table: "Alternativas",
                column: "QuestaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_ModuloId",
                table: "Aulas",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidaturas_UsuarioId",
                table: "Candidaturas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidaturas_VagaId",
                table: "Candidaturas",
                column: "VagaId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificados_TreinamentoId",
                table: "Certificados",
                column: "TreinamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificados_UsuarioId",
                table: "Certificados",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Midias_AulaId",
                table: "Midias",
                column: "AulaId");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_UsuarioId",
                table: "Notas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Progress_AulaId",
                table: "Progress",
                column: "AulaId");

            migrationBuilder.CreateIndex(
                name: "IX_Progress_UsuarioId",
                table: "Progress",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Questoes_AulaId",
                table: "Questoes",
                column: "AulaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_UsuarioId",
                table: "Reports",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_UseProva_UsuarioId",
                table: "UseProva",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_UseTreinamentos_TreinamentoId",
                table: "UseTreinamentos",
                column: "TreinamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_UseTreinamentos_UsuarioId",
                table: "UseTreinamentos",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alternativas");

            migrationBuilder.DropTable(
                name: "Candidaturas");

            migrationBuilder.DropTable(
                name: "Certificados");

            migrationBuilder.DropTable(
                name: "Midias");

            migrationBuilder.DropTable(
                name: "Notas");

            migrationBuilder.DropTable(
                name: "Progress");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "UseProva");

            migrationBuilder.DropTable(
                name: "UseTreinamentos");

            migrationBuilder.DropTable(
                name: "Questoes");

            migrationBuilder.DropTable(
                name: "Vagas");

            migrationBuilder.DropTable(
                name: "Aulas");
        }
    }
}
