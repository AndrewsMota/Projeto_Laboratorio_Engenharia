using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Bioterio_EnderecoBioterio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bioterios",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(300)", nullable: false),
                    Telefone = table.Column<string>(type: "varchar(11)", nullable: false),
                    Cnpj = table.Column<string>(type: "varchar(14)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bioterios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnderecosBioterios",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BioterioId = table.Column<Guid>(nullable: false),
                    Logradouro = table.Column<string>(type: "varchar(200)", nullable: false),
                    Numero = table.Column<string>(type: "varchar(6)", nullable: false),
                    Complemento = table.Column<string>(type: "varchar(300)", nullable: true),
                    Cep = table.Column<string>(type: "varchar(8)", nullable: false),
                    Bairro = table.Column<string>(type: "varchar(100)", nullable: false),
                    Cidade = table.Column<string>(type: "varchar(100)", nullable: false),
                    Estado = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnderecosBioterios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnderecosBioterios_Bioterios_BioterioId",
                        column: x => x.BioterioId,
                        principalTable: "Bioterios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnderecosBioterios_BioterioId",
                table: "EnderecosBioterios",
                column: "BioterioId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnderecosBioterios");

            migrationBuilder.DropTable(
                name: "Bioterios");
        }
    }
}
