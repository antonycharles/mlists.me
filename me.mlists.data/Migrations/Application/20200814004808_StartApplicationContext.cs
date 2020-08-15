using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace me.mlists.data.Migrations.Application
{
    public partial class StartApplicationContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mltb_monsters",
                columns: table => new
                {
                    monster_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mltb_monsters", x => x.monster_id);
                });

            migrationBuilder.CreateTable(
                name: "mltb_listas",
                columns: table => new
                {
                    lista_id = table.Column<string>(nullable: false),
                    CriadoPorId = table.Column<string>(maxLength: 255, nullable: false),
                    MonsterId = table.Column<int>(nullable: false),
                    ListaStatus = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mltb_listas", x => x.lista_id);
                    table.ForeignKey(
                        name: "FK_mltb_listas_mltb_monsters_MonsterId",
                        column: x => x.MonsterId,
                        principalTable: "mltb_monsters",
                        principalColumn: "monster_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mltb_convidados",
                columns: table => new
                {
                    convidado_id = table.Column<string>(nullable: false),
                    ListaId = table.Column<string>(nullable: false),
                    ConvidadoPorId = table.Column<string>(nullable: false),
                    email_convidado = table.Column<string>(maxLength: 100, nullable: false),
                    DataEnvio = table.Column<DateTime>(nullable: false),
                    DataResposta = table.Column<DateTime>(nullable: true),
                    IsAceitou = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mltb_convidados", x => x.convidado_id);
                    table.ForeignKey(
                        name: "FK_mltb_convidados_mltb_listas_ListaId",
                        column: x => x.ListaId,
                        principalTable: "mltb_listas",
                        principalColumn: "lista_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mltb_lista_secoes",
                columns: table => new
                {
                    lista_secao_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ListaId = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mltb_lista_secoes", x => x.lista_secao_id);
                    table.ForeignKey(
                        name: "FK_mltb_lista_secoes_mltb_listas_ListaId",
                        column: x => x.ListaId,
                        principalTable: "mltb_listas",
                        principalColumn: "lista_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mltb_participantes",
                columns: table => new
                {
                    participante_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(maxLength: 255, nullable: false),
                    ListaId = table.Column<string>(nullable: false),
                    ParticipantePerfil = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mltb_participantes", x => x.participante_id);
                    table.ForeignKey(
                        name: "FK_mltb_participantes_mltb_listas_ListaId",
                        column: x => x.ListaId,
                        principalTable: "mltb_listas",
                        principalColumn: "lista_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mltb_tarefas",
                columns: table => new
                {
                    tarefa_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ListaId = table.Column<string>(nullable: true),
                    ParticipanteId = table.Column<int>(nullable: false),
                    ListaSecaoId = table.Column<int>(nullable: true),
                    Nome = table.Column<string>(maxLength: 250, nullable: false),
                    DataVencimento = table.Column<DateTime>(nullable: true),
                    IsChecked = table.Column<bool>(nullable: false),
                    IsLixeira = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mltb_tarefas", x => x.tarefa_id);
                    table.ForeignKey(
                        name: "FK_mltb_tarefas_mltb_listas_ListaId",
                        column: x => x.ListaId,
                        principalTable: "mltb_listas",
                        principalColumn: "lista_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_mltb_tarefas_mltb_lista_secoes_ListaSecaoId",
                        column: x => x.ListaSecaoId,
                        principalTable: "mltb_lista_secoes",
                        principalColumn: "lista_secao_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_mltb_tarefas_mltb_participantes_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "mltb_participantes",
                        principalColumn: "participante_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mltb_convidados_ListaId",
                table: "mltb_convidados",
                column: "ListaId");

            migrationBuilder.CreateIndex(
                name: "IX_mltb_lista_secoes_ListaId",
                table: "mltb_lista_secoes",
                column: "ListaId");

            migrationBuilder.CreateIndex(
                name: "IX_mltb_listas_MonsterId",
                table: "mltb_listas",
                column: "MonsterId");

            migrationBuilder.CreateIndex(
                name: "IX_mltb_participantes_ListaId",
                table: "mltb_participantes",
                column: "ListaId");

            migrationBuilder.CreateIndex(
                name: "IX_mltb_tarefas_ListaId",
                table: "mltb_tarefas",
                column: "ListaId");

            migrationBuilder.CreateIndex(
                name: "IX_mltb_tarefas_ListaSecaoId",
                table: "mltb_tarefas",
                column: "ListaSecaoId");

            migrationBuilder.CreateIndex(
                name: "IX_mltb_tarefas_ParticipanteId",
                table: "mltb_tarefas",
                column: "ParticipanteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mltb_convidados");

            migrationBuilder.DropTable(
                name: "mltb_tarefas");

            migrationBuilder.DropTable(
                name: "mltb_lista_secoes");

            migrationBuilder.DropTable(
                name: "mltb_participantes");

            migrationBuilder.DropTable(
                name: "mltb_listas");

            migrationBuilder.DropTable(
                name: "mltb_monsters");
        }
    }
}
