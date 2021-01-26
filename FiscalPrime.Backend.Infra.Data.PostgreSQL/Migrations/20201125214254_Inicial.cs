using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FiscalPrime.Backend.Infra.Data.PostgreSQL.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "empresa",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    NumeroCNPJCPF = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    NomeFantasia = table.Column<string>(nullable: true),
                    Endereco = table.Column<string>(nullable: true),
                    Numero = table.Column<string>(nullable: true),
                    Complemento = table.Column<string>(nullable: true),
                    Bairro = table.Column<string>(nullable: true),
                    CodigoMunicipioIBGE = table.Column<int>(nullable: false),
                    CodigoUF = table.Column<short>(nullable: false),
                    Cep = table.Column<string>(nullable: true),
                    CodigoPais = table.Column<short>(nullable: false),
                    NomePais = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    InscricaoEstadual = table.Column<string>(nullable: true),
                    InscricaoEstadualST = table.Column<string>(nullable: true),
                    InscricaoMunicipal = table.Column<string>(nullable: true),
                    CodigoCNAE = table.Column<string>(nullable: true),
                    CodigoCRT = table.Column<short>(nullable: false),
                    CodigoSuframa = table.Column<string>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    DataInclusao = table.Column<TimeSpan>(nullable: false),
                    DataUltimaAlteracao = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empresa", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "empresa");
        }
    }
}
