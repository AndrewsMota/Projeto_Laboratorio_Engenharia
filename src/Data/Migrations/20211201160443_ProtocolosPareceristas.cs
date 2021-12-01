using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ProtocolosPareceristas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Protocolos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    Justificativa = table.Column<string>(type: "varchar(500)", nullable: false),
                    ResumoPt = table.Column<string>(type: "varchar(1000)", nullable: false),
                    ResumoEn = table.Column<string>(type: "varchar(1000)", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "date", nullable: false),
                    DataTermino = table.Column<DateTime>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Protocolos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Protocolos_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProtocoloPareceristas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProtocoloId = table.Column<Guid>(nullable: false),
                    PareceristaId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProtocoloPareceristas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProtocoloPareceristas_AspNetUsers_PareceristaId",
                        column: x => x.PareceristaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProtocoloPareceristas_Protocolos_ProtocoloId",
                        column: x => x.ProtocoloId,
                        principalTable: "Protocolos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProtocolosEspecies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProtocoloId = table.Column<Guid>(nullable: false),
                    EspecieId = table.Column<Guid>(nullable: false),
                    Quantidade = table.Column<string>(type: "varchar(5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProtocolosEspecies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProtocolosEspecies_Especies_EspecieId",
                        column: x => x.EspecieId,
                        principalTable: "Especies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProtocolosEspecies_Protocolos_ProtocoloId",
                        column: x => x.ProtocoloId,
                        principalTable: "Protocolos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProtocoloPareceristas_PareceristaId",
                table: "ProtocoloPareceristas",
                column: "PareceristaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProtocoloPareceristas_ProtocoloId",
                table: "ProtocoloPareceristas",
                column: "ProtocoloId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Protocolos_ApplicationUserId",
                table: "Protocolos",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProtocolosEspecies_EspecieId",
                table: "ProtocolosEspecies",
                column: "EspecieId");

            migrationBuilder.CreateIndex(
                name: "IX_ProtocolosEspecies_ProtocoloId",
                table: "ProtocolosEspecies",
                column: "ProtocoloId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProtocoloPareceristas");

            migrationBuilder.DropTable(
                name: "ProtocolosEspecies");

            migrationBuilder.DropTable(
                name: "Protocolos");
        }
    }
}
