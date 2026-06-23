using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TESTEMINHAAPI.Migrations
{
    /// <inheritdoc />
    public partial class licences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alternativas_Questoes_QuestaoId",
                table: "Alternativas");

            migrationBuilder.DropForeignKey(
                name: "FK_Aulas_Modulos_ModuloId",
                table: "Aulas");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidaturas_Usuarios_UsuarioId",
                table: "Candidaturas");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidaturas_Vagas_VagaId",
                table: "Candidaturas");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificados_Treinamentos_TreinamentoId",
                table: "Certificados");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificados_Usuarios_UsuarioId",
                table: "Certificados");

            migrationBuilder.DropForeignKey(
                name: "FK_Midias_Aulas_AulaId",
                table: "Midias");

            migrationBuilder.DropForeignKey(
                name: "FK_Modulos_Treinamentos_TreinamentoId",
                table: "Modulos");

            migrationBuilder.DropForeignKey(
                name: "FK_Notas_Usuarios_UsuarioId",
                table: "Notas");

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Aulas_AulaId",
                table: "Progress");

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Usuarios_UsuarioId",
                table: "Progress");

            migrationBuilder.DropForeignKey(
                name: "FK_Questoes_Aulas_AulaId",
                table: "Questoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Usuarios_UsuarioId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_UseProva_Usuarios_UsuarioId",
                table: "UseProva");

            migrationBuilder.DropForeignKey(
                name: "FK_UseTreinamentos_Treinamentos_TreinamentoId",
                table: "UseTreinamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_UseTreinamentos_Usuarios_UsuarioId",
                table: "UseTreinamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioTreinamentos_Treinamentos_TreinamentoId",
                table: "UsuarioTreinamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioTreinamentos_Usuarios_UsuarioId",
                table: "UsuarioTreinamentos");

            migrationBuilder.RenameColumn(
                name: "Titulo",
                table: "Vagas",
                newName: "titulo");

            migrationBuilder.RenameColumn(
                name: "Quantidade",
                table: "Vagas",
                newName: "quantidade");

            migrationBuilder.RenameColumn(
                name: "Localizacao",
                table: "Vagas",
                newName: "localizacao");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Vagas",
                newName: "descricao");

            migrationBuilder.RenameColumn(
                name: "Criado",
                table: "Vagas",
                newName: "criado");

            migrationBuilder.RenameColumn(
                name: "Ativa",
                table: "Vagas",
                newName: "ativa");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Vagas",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "UsuarioTreinamentos",
                newName: "usuarioid");

            migrationBuilder.RenameColumn(
                name: "TreinamentoId",
                table: "UsuarioTreinamentos",
                newName: "treinamentoid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UsuarioTreinamentos",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioTreinamentos_UsuarioId",
                table: "UsuarioTreinamentos",
                newName: "IX_UsuarioTreinamentos_usuarioid");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioTreinamentos_TreinamentoId",
                table: "UsuarioTreinamentos",
                newName: "IX_UsuarioTreinamentos_treinamentoid");

            migrationBuilder.RenameColumn(
                name: "Token",
                table: "Usuarios",
                newName: "token");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Usuarios",
                newName: "tipo");

            migrationBuilder.RenameColumn(
                name: "Senha",
                table: "Usuarios",
                newName: "senha");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Usuarios",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Usuarios",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Ativo",
                table: "Usuarios",
                newName: "ativo");

            migrationBuilder.RenameColumn(
                name: "Acesso",
                table: "Usuarios",
                newName: "acesso");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Usuarios",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "UseTreinamentos",
                newName: "usuarioid");

            migrationBuilder.RenameColumn(
                name: "TreinamentoId",
                table: "UseTreinamentos",
                newName: "treinamentoid");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "UseTreinamentos",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UseTreinamentos",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "MatriculadoEm",
                table: "UseTreinamentos",
                newName: "matriculado_em");

            migrationBuilder.RenameIndex(
                name: "IX_UseTreinamentos_UsuarioId",
                table: "UseTreinamentos",
                newName: "IX_UseTreinamentos_usuarioid");

            migrationBuilder.RenameIndex(
                name: "IX_UseTreinamentos_TreinamentoId",
                table: "UseTreinamentos",
                newName: "IX_UseTreinamentos_treinamentoid");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "UseProva",
                newName: "usuarioid");

            migrationBuilder.RenameColumn(
                name: "Nota",
                table: "UseProva",
                newName: "nota");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UseProva",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RealizadoEm",
                table: "UseProva",
                newName: "realizado_em");

            migrationBuilder.RenameColumn(
                name: "ProvaId",
                table: "UseProva",
                newName: "usuario_id");

            migrationBuilder.RenameIndex(
                name: "IX_UseProva_UsuarioId",
                table: "UseProva",
                newName: "IX_UseProva_usuarioid");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Treinamentos",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Criado",
                table: "Treinamentos",
                newName: "criado");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Treinamentos",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Reports",
                newName: "usuarioid");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Reports",
                newName: "tipo");

            migrationBuilder.RenameColumn(
                name: "Mensagem",
                table: "Reports",
                newName: "mensagem");

            migrationBuilder.RenameColumn(
                name: "Criado",
                table: "Reports",
                newName: "criado");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Reports",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_UsuarioId",
                table: "Reports",
                newName: "IX_Reports_usuarioid");

            migrationBuilder.RenameColumn(
                name: "Texto",
                table: "Questoes",
                newName: "texto");

            migrationBuilder.RenameColumn(
                name: "Criado",
                table: "Questoes",
                newName: "criado");

            migrationBuilder.RenameColumn(
                name: "AulaId",
                table: "Questoes",
                newName: "aulaid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Questoes",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Questoes_AulaId",
                table: "Questoes",
                newName: "IX_Questoes_aulaid");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Progress",
                newName: "usuarioid");

            migrationBuilder.RenameColumn(
                name: "Percentual",
                table: "Progress",
                newName: "percentual");

            migrationBuilder.RenameColumn(
                name: "AulaId",
                table: "Progress",
                newName: "aulaid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Progress",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TempoSegundos",
                table: "Progress",
                newName: "usuario_id");

            migrationBuilder.RenameColumn(
                name: "AtualizadoEm",
                table: "Progress",
                newName: "atualizado_em");

            migrationBuilder.RenameIndex(
                name: "IX_Progress_UsuarioId",
                table: "Progress",
                newName: "IX_Progress_usuarioid");

            migrationBuilder.RenameIndex(
                name: "IX_Progress_AulaId",
                table: "Progress",
                newName: "IX_Progress_aulaid");

            migrationBuilder.RenameColumn(
                name: "Vaga",
                table: "Noticias",
                newName: "vaga");

            migrationBuilder.RenameColumn(
                name: "Titulo",
                table: "Noticias",
                newName: "titulo");

            migrationBuilder.RenameColumn(
                name: "Conteudo",
                table: "Noticias",
                newName: "conteudo");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Noticias",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "Notas",
                newName: "valor");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Notas",
                newName: "usuarioid");

            migrationBuilder.RenameColumn(
                name: "Criado",
                table: "Notas",
                newName: "criado");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Notas",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TreinamentoId",
                table: "Notas",
                newName: "treinamento_id");

            migrationBuilder.RenameColumn(
                name: "ProvaId",
                table: "Notas",
                newName: "prova_id");

            migrationBuilder.RenameIndex(
                name: "IX_Notas_UsuarioId",
                table: "Notas",
                newName: "IX_Notas_usuarioid");

            migrationBuilder.RenameColumn(
                name: "TreinamentoId",
                table: "Modulos",
                newName: "treinamentoid");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Modulos",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Modulos",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Modulos_TreinamentoId",
                table: "Modulos",
                newName: "IX_Modulos_treinamentoid");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Midias",
                newName: "url");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Midias",
                newName: "tipo");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Midias",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Criado",
                table: "Midias",
                newName: "criado");

            migrationBuilder.RenameColumn(
                name: "AulaId",
                table: "Midias",
                newName: "aulaid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Midias",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Midias_AulaId",
                table: "Midias",
                newName: "IX_Midias_aulaid");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Certificados",
                newName: "usuarioid");

            migrationBuilder.RenameColumn(
                name: "TreinamentoId",
                table: "Certificados",
                newName: "treinamentoid");

            migrationBuilder.RenameColumn(
                name: "Codigo",
                table: "Certificados",
                newName: "codigo");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Certificados",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "EmitidoEm",
                table: "Certificados",
                newName: "emitido_em");

            migrationBuilder.RenameIndex(
                name: "IX_Certificados_UsuarioId",
                table: "Certificados",
                newName: "IX_Certificados_usuarioid");

            migrationBuilder.RenameIndex(
                name: "IX_Certificados_TreinamentoId",
                table: "Certificados",
                newName: "IX_Certificados_treinamentoid");

            migrationBuilder.RenameColumn(
                name: "VagaId",
                table: "Candidaturas",
                newName: "vagaid");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Candidaturas",
                newName: "usuarioid");

            migrationBuilder.RenameColumn(
                name: "Telefone",
                table: "Candidaturas",
                newName: "telefone");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Candidaturas",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Candidaturas",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Candidaturas",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Criado",
                table: "Candidaturas",
                newName: "criado");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Candidaturas",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "CurriculoUrl",
                table: "Candidaturas",
                newName: "curriculo_url");

            migrationBuilder.RenameIndex(
                name: "IX_Candidaturas_VagaId",
                table: "Candidaturas",
                newName: "IX_Candidaturas_vagaid");

            migrationBuilder.RenameIndex(
                name: "IX_Candidaturas_UsuarioId",
                table: "Candidaturas",
                newName: "IX_Candidaturas_usuarioid");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Aulas",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "ModuloId",
                table: "Aulas",
                newName: "moduloid");

            migrationBuilder.RenameColumn(
                name: "Criado",
                table: "Aulas",
                newName: "criado");

            migrationBuilder.RenameColumn(
                name: "Conteudo",
                table: "Aulas",
                newName: "conteudo");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Aulas",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Aulas_ModuloId",
                table: "Aulas",
                newName: "IX_Aulas_moduloid");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Alternativas",
                newName: "url");

            migrationBuilder.RenameColumn(
                name: "Texto",
                table: "Alternativas",
                newName: "texto");

            migrationBuilder.RenameColumn(
                name: "QuestaoId",
                table: "Alternativas",
                newName: "questaoid");

            migrationBuilder.RenameColumn(
                name: "Ordem",
                table: "Alternativas",
                newName: "ordem");

            migrationBuilder.RenameColumn(
                name: "Criado",
                table: "Alternativas",
                newName: "criado");

            migrationBuilder.RenameColumn(
                name: "Correta",
                table: "Alternativas",
                newName: "correta");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Alternativas",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Alternativas_QuestaoId",
                table: "Alternativas",
                newName: "IX_Alternativas_questaoid");

            migrationBuilder.AddColumn<int>(
                name: "treinamento_id",
                table: "UsuarioTreinamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "usuario_id",
                table: "UsuarioTreinamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "treinamento_id",
                table: "UseTreinamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "usuario_id",
                table: "UseTreinamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "prova_id",
                table: "UseProva",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "usuario_id",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "aula_id",
                table: "Questoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "aula_id",
                table: "Progress",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "tempo_segundos",
                table: "Progress",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "usuario_id",
                table: "Notas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "treinamento_id",
                table: "Modulos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "aula_id",
                table: "Midias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "treinamento_id",
                table: "Certificados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "usuario_id",
                table: "Certificados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "usuarioid",
                table: "Candidaturas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "usuario_id",
                table: "Candidaturas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "vaga_id",
                table: "Candidaturas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "modulo_id",
                table: "Aulas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "questao_id",
                table: "Alternativas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Licencas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    codigo = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    usuario_id = table.Column<int>(type: "int", nullable: true),
                    usuarioid = table.Column<int>(type: "int", nullable: false),
                    criado_em = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    validade_em = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ativo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    produto = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    preco = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licencas", x => x.id);
                    table.ForeignKey(
                        name: "FK_Licencas_Usuarios_usuarioid",
                        column: x => x.usuarioid,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Licencas_usuarioid",
                table: "Licencas",
                column: "usuarioid");

            migrationBuilder.AddForeignKey(
                name: "FK_Alternativas_Questoes_questaoid",
                table: "Alternativas",
                column: "questaoid",
                principalTable: "Questoes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_Modulos_moduloid",
                table: "Aulas",
                column: "moduloid",
                principalTable: "Modulos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidaturas_Usuarios_usuarioid",
                table: "Candidaturas",
                column: "usuarioid",
                principalTable: "Usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidaturas_Vagas_vagaid",
                table: "Candidaturas",
                column: "vagaid",
                principalTable: "Vagas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificados_Treinamentos_treinamentoid",
                table: "Certificados",
                column: "treinamentoid",
                principalTable: "Treinamentos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificados_Usuarios_usuarioid",
                table: "Certificados",
                column: "usuarioid",
                principalTable: "Usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Midias_Aulas_aulaid",
                table: "Midias",
                column: "aulaid",
                principalTable: "Aulas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modulos_Treinamentos_treinamentoid",
                table: "Modulos",
                column: "treinamentoid",
                principalTable: "Treinamentos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_Usuarios_usuarioid",
                table: "Notas",
                column: "usuarioid",
                principalTable: "Usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Aulas_aulaid",
                table: "Progress",
                column: "aulaid",
                principalTable: "Aulas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Usuarios_usuarioid",
                table: "Progress",
                column: "usuarioid",
                principalTable: "Usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questoes_Aulas_aulaid",
                table: "Questoes",
                column: "aulaid",
                principalTable: "Aulas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Usuarios_usuarioid",
                table: "Reports",
                column: "usuarioid",
                principalTable: "Usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UseProva_Usuarios_usuarioid",
                table: "UseProva",
                column: "usuarioid",
                principalTable: "Usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UseTreinamentos_Treinamentos_treinamentoid",
                table: "UseTreinamentos",
                column: "treinamentoid",
                principalTable: "Treinamentos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UseTreinamentos_Usuarios_usuarioid",
                table: "UseTreinamentos",
                column: "usuarioid",
                principalTable: "Usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioTreinamentos_Treinamentos_treinamentoid",
                table: "UsuarioTreinamentos",
                column: "treinamentoid",
                principalTable: "Treinamentos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioTreinamentos_Usuarios_usuarioid",
                table: "UsuarioTreinamentos",
                column: "usuarioid",
                principalTable: "Usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alternativas_Questoes_questaoid",
                table: "Alternativas");

            migrationBuilder.DropForeignKey(
                name: "FK_Aulas_Modulos_moduloid",
                table: "Aulas");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidaturas_Usuarios_usuarioid",
                table: "Candidaturas");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidaturas_Vagas_vagaid",
                table: "Candidaturas");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificados_Treinamentos_treinamentoid",
                table: "Certificados");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificados_Usuarios_usuarioid",
                table: "Certificados");

            migrationBuilder.DropForeignKey(
                name: "FK_Midias_Aulas_aulaid",
                table: "Midias");

            migrationBuilder.DropForeignKey(
                name: "FK_Modulos_Treinamentos_treinamentoid",
                table: "Modulos");

            migrationBuilder.DropForeignKey(
                name: "FK_Notas_Usuarios_usuarioid",
                table: "Notas");

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Aulas_aulaid",
                table: "Progress");

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Usuarios_usuarioid",
                table: "Progress");

            migrationBuilder.DropForeignKey(
                name: "FK_Questoes_Aulas_aulaid",
                table: "Questoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Usuarios_usuarioid",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_UseProva_Usuarios_usuarioid",
                table: "UseProva");

            migrationBuilder.DropForeignKey(
                name: "FK_UseTreinamentos_Treinamentos_treinamentoid",
                table: "UseTreinamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_UseTreinamentos_Usuarios_usuarioid",
                table: "UseTreinamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioTreinamentos_Treinamentos_treinamentoid",
                table: "UsuarioTreinamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioTreinamentos_Usuarios_usuarioid",
                table: "UsuarioTreinamentos");

            migrationBuilder.DropTable(
                name: "Licencas");

            migrationBuilder.DropColumn(
                name: "treinamento_id",
                table: "UsuarioTreinamentos");

            migrationBuilder.DropColumn(
                name: "usuario_id",
                table: "UsuarioTreinamentos");

            migrationBuilder.DropColumn(
                name: "treinamento_id",
                table: "UseTreinamentos");

            migrationBuilder.DropColumn(
                name: "usuario_id",
                table: "UseTreinamentos");

            migrationBuilder.DropColumn(
                name: "prova_id",
                table: "UseProva");

            migrationBuilder.DropColumn(
                name: "usuario_id",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "aula_id",
                table: "Questoes");

            migrationBuilder.DropColumn(
                name: "aula_id",
                table: "Progress");

            migrationBuilder.DropColumn(
                name: "tempo_segundos",
                table: "Progress");

            migrationBuilder.DropColumn(
                name: "usuario_id",
                table: "Notas");

            migrationBuilder.DropColumn(
                name: "treinamento_id",
                table: "Modulos");

            migrationBuilder.DropColumn(
                name: "aula_id",
                table: "Midias");

            migrationBuilder.DropColumn(
                name: "treinamento_id",
                table: "Certificados");

            migrationBuilder.DropColumn(
                name: "usuario_id",
                table: "Certificados");

            migrationBuilder.DropColumn(
                name: "usuario_id",
                table: "Candidaturas");

            migrationBuilder.DropColumn(
                name: "vaga_id",
                table: "Candidaturas");

            migrationBuilder.DropColumn(
                name: "modulo_id",
                table: "Aulas");

            migrationBuilder.DropColumn(
                name: "questao_id",
                table: "Alternativas");

            migrationBuilder.RenameColumn(
                name: "titulo",
                table: "Vagas",
                newName: "Titulo");

            migrationBuilder.RenameColumn(
                name: "quantidade",
                table: "Vagas",
                newName: "Quantidade");

            migrationBuilder.RenameColumn(
                name: "localizacao",
                table: "Vagas",
                newName: "Localizacao");

            migrationBuilder.RenameColumn(
                name: "descricao",
                table: "Vagas",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "criado",
                table: "Vagas",
                newName: "Criado");

            migrationBuilder.RenameColumn(
                name: "ativa",
                table: "Vagas",
                newName: "Ativa");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Vagas",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "usuarioid",
                table: "UsuarioTreinamentos",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "treinamentoid",
                table: "UsuarioTreinamentos",
                newName: "TreinamentoId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "UsuarioTreinamentos",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioTreinamentos_usuarioid",
                table: "UsuarioTreinamentos",
                newName: "IX_UsuarioTreinamentos_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioTreinamentos_treinamentoid",
                table: "UsuarioTreinamentos",
                newName: "IX_UsuarioTreinamentos_TreinamentoId");

            migrationBuilder.RenameColumn(
                name: "token",
                table: "Usuarios",
                newName: "Token");

            migrationBuilder.RenameColumn(
                name: "tipo",
                table: "Usuarios",
                newName: "Tipo");

            migrationBuilder.RenameColumn(
                name: "senha",
                table: "Usuarios",
                newName: "Senha");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Usuarios",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Usuarios",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "ativo",
                table: "Usuarios",
                newName: "Ativo");

            migrationBuilder.RenameColumn(
                name: "acesso",
                table: "Usuarios",
                newName: "Acesso");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Usuarios",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "usuarioid",
                table: "UseTreinamentos",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "treinamentoid",
                table: "UseTreinamentos",
                newName: "TreinamentoId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "UseTreinamentos",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "UseTreinamentos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "matriculado_em",
                table: "UseTreinamentos",
                newName: "MatriculadoEm");

            migrationBuilder.RenameIndex(
                name: "IX_UseTreinamentos_usuarioid",
                table: "UseTreinamentos",
                newName: "IX_UseTreinamentos_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_UseTreinamentos_treinamentoid",
                table: "UseTreinamentos",
                newName: "IX_UseTreinamentos_TreinamentoId");

            migrationBuilder.RenameColumn(
                name: "usuarioid",
                table: "UseProva",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "nota",
                table: "UseProva",
                newName: "Nota");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "UseProva",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "usuario_id",
                table: "UseProva",
                newName: "ProvaId");

            migrationBuilder.RenameColumn(
                name: "realizado_em",
                table: "UseProva",
                newName: "RealizadoEm");

            migrationBuilder.RenameIndex(
                name: "IX_UseProva_usuarioid",
                table: "UseProva",
                newName: "IX_UseProva_UsuarioId");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Treinamentos",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "criado",
                table: "Treinamentos",
                newName: "Criado");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Treinamentos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "usuarioid",
                table: "Reports",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "tipo",
                table: "Reports",
                newName: "Tipo");

            migrationBuilder.RenameColumn(
                name: "mensagem",
                table: "Reports",
                newName: "Mensagem");

            migrationBuilder.RenameColumn(
                name: "criado",
                table: "Reports",
                newName: "Criado");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Reports",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_usuarioid",
                table: "Reports",
                newName: "IX_Reports_UsuarioId");

            migrationBuilder.RenameColumn(
                name: "texto",
                table: "Questoes",
                newName: "Texto");

            migrationBuilder.RenameColumn(
                name: "criado",
                table: "Questoes",
                newName: "Criado");

            migrationBuilder.RenameColumn(
                name: "aulaid",
                table: "Questoes",
                newName: "AulaId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Questoes",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Questoes_aulaid",
                table: "Questoes",
                newName: "IX_Questoes_AulaId");

            migrationBuilder.RenameColumn(
                name: "usuarioid",
                table: "Progress",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "percentual",
                table: "Progress",
                newName: "Percentual");

            migrationBuilder.RenameColumn(
                name: "aulaid",
                table: "Progress",
                newName: "AulaId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Progress",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "usuario_id",
                table: "Progress",
                newName: "TempoSegundos");

            migrationBuilder.RenameColumn(
                name: "atualizado_em",
                table: "Progress",
                newName: "AtualizadoEm");

            migrationBuilder.RenameIndex(
                name: "IX_Progress_usuarioid",
                table: "Progress",
                newName: "IX_Progress_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Progress_aulaid",
                table: "Progress",
                newName: "IX_Progress_AulaId");

            migrationBuilder.RenameColumn(
                name: "vaga",
                table: "Noticias",
                newName: "Vaga");

            migrationBuilder.RenameColumn(
                name: "titulo",
                table: "Noticias",
                newName: "Titulo");

            migrationBuilder.RenameColumn(
                name: "conteudo",
                table: "Noticias",
                newName: "Conteudo");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Noticias",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "valor",
                table: "Notas",
                newName: "Valor");

            migrationBuilder.RenameColumn(
                name: "usuarioid",
                table: "Notas",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "criado",
                table: "Notas",
                newName: "Criado");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Notas",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "treinamento_id",
                table: "Notas",
                newName: "TreinamentoId");

            migrationBuilder.RenameColumn(
                name: "prova_id",
                table: "Notas",
                newName: "ProvaId");

            migrationBuilder.RenameIndex(
                name: "IX_Notas_usuarioid",
                table: "Notas",
                newName: "IX_Notas_UsuarioId");

            migrationBuilder.RenameColumn(
                name: "treinamentoid",
                table: "Modulos",
                newName: "TreinamentoId");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Modulos",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Modulos",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Modulos_treinamentoid",
                table: "Modulos",
                newName: "IX_Modulos_TreinamentoId");

            migrationBuilder.RenameColumn(
                name: "url",
                table: "Midias",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "tipo",
                table: "Midias",
                newName: "Tipo");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Midias",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "criado",
                table: "Midias",
                newName: "Criado");

            migrationBuilder.RenameColumn(
                name: "aulaid",
                table: "Midias",
                newName: "AulaId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Midias",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Midias_aulaid",
                table: "Midias",
                newName: "IX_Midias_AulaId");

            migrationBuilder.RenameColumn(
                name: "usuarioid",
                table: "Certificados",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "treinamentoid",
                table: "Certificados",
                newName: "TreinamentoId");

            migrationBuilder.RenameColumn(
                name: "codigo",
                table: "Certificados",
                newName: "Codigo");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Certificados",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "emitido_em",
                table: "Certificados",
                newName: "EmitidoEm");

            migrationBuilder.RenameIndex(
                name: "IX_Certificados_usuarioid",
                table: "Certificados",
                newName: "IX_Certificados_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Certificados_treinamentoid",
                table: "Certificados",
                newName: "IX_Certificados_TreinamentoId");

            migrationBuilder.RenameColumn(
                name: "vagaid",
                table: "Candidaturas",
                newName: "VagaId");

            migrationBuilder.RenameColumn(
                name: "usuarioid",
                table: "Candidaturas",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "telefone",
                table: "Candidaturas",
                newName: "Telefone");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Candidaturas",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Candidaturas",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Candidaturas",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "criado",
                table: "Candidaturas",
                newName: "Criado");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Candidaturas",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "curriculo_url",
                table: "Candidaturas",
                newName: "CurriculoUrl");

            migrationBuilder.RenameIndex(
                name: "IX_Candidaturas_vagaid",
                table: "Candidaturas",
                newName: "IX_Candidaturas_VagaId");

            migrationBuilder.RenameIndex(
                name: "IX_Candidaturas_usuarioid",
                table: "Candidaturas",
                newName: "IX_Candidaturas_UsuarioId");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Aulas",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "moduloid",
                table: "Aulas",
                newName: "ModuloId");

            migrationBuilder.RenameColumn(
                name: "criado",
                table: "Aulas",
                newName: "Criado");

            migrationBuilder.RenameColumn(
                name: "conteudo",
                table: "Aulas",
                newName: "Conteudo");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Aulas",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Aulas_moduloid",
                table: "Aulas",
                newName: "IX_Aulas_ModuloId");

            migrationBuilder.RenameColumn(
                name: "url",
                table: "Alternativas",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "texto",
                table: "Alternativas",
                newName: "Texto");

            migrationBuilder.RenameColumn(
                name: "questaoid",
                table: "Alternativas",
                newName: "QuestaoId");

            migrationBuilder.RenameColumn(
                name: "ordem",
                table: "Alternativas",
                newName: "Ordem");

            migrationBuilder.RenameColumn(
                name: "criado",
                table: "Alternativas",
                newName: "Criado");

            migrationBuilder.RenameColumn(
                name: "correta",
                table: "Alternativas",
                newName: "Correta");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Alternativas",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Alternativas_questaoid",
                table: "Alternativas",
                newName: "IX_Alternativas_QuestaoId");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Candidaturas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Alternativas_Questoes_QuestaoId",
                table: "Alternativas",
                column: "QuestaoId",
                principalTable: "Questoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_Modulos_ModuloId",
                table: "Aulas",
                column: "ModuloId",
                principalTable: "Modulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidaturas_Usuarios_UsuarioId",
                table: "Candidaturas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidaturas_Vagas_VagaId",
                table: "Candidaturas",
                column: "VagaId",
                principalTable: "Vagas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificados_Treinamentos_TreinamentoId",
                table: "Certificados",
                column: "TreinamentoId",
                principalTable: "Treinamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificados_Usuarios_UsuarioId",
                table: "Certificados",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Midias_Aulas_AulaId",
                table: "Midias",
                column: "AulaId",
                principalTable: "Aulas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modulos_Treinamentos_TreinamentoId",
                table: "Modulos",
                column: "TreinamentoId",
                principalTable: "Treinamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_Usuarios_UsuarioId",
                table: "Notas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Aulas_AulaId",
                table: "Progress",
                column: "AulaId",
                principalTable: "Aulas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Usuarios_UsuarioId",
                table: "Progress",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questoes_Aulas_AulaId",
                table: "Questoes",
                column: "AulaId",
                principalTable: "Aulas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Usuarios_UsuarioId",
                table: "Reports",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UseProva_Usuarios_UsuarioId",
                table: "UseProva",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UseTreinamentos_Treinamentos_TreinamentoId",
                table: "UseTreinamentos",
                column: "TreinamentoId",
                principalTable: "Treinamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UseTreinamentos_Usuarios_UsuarioId",
                table: "UseTreinamentos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioTreinamentos_Treinamentos_TreinamentoId",
                table: "UsuarioTreinamentos",
                column: "TreinamentoId",
                principalTable: "Treinamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioTreinamentos_Usuarios_UsuarioId",
                table: "UsuarioTreinamentos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
