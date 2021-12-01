using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Protocolo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Protocolos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
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
                name: "ProtocolosEspecies");

            migrationBuilder.DropTable(
                name: "Protocolos");
        }
    }
}
