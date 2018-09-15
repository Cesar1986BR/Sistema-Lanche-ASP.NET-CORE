using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SitemaLanche.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoriaNome = table.Column<string>(maxLength: 100, nullable: true),
                    Descricao = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Lanches",
                columns: table => new
                {
                    lancheId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoriaId = table.Column<int>(nullable: false),
                    DescricaoCurta = table.Column<string>(maxLength: 100, nullable: true),
                    DescricaoDetalhada = table.Column<string>(maxLength: 255, nullable: true),
                    EmEstoque = table.Column<bool>(nullable: false),
                    ImagemThumbnailUrl = table.Column<string>(maxLength: 200, nullable: true),
                    ImagemUrl = table.Column<string>(maxLength: 200, nullable: true),
                    IsLanchePreferido = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: true),
                    Preco = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lanches", x => x.lancheId);
                    table.ForeignKey(
                        name: "FK_Lanches_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarrinhoCompraItens",
                columns: table => new
                {
                    CarrinhoCompraItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CarrinhoCompraId = table.Column<string>(maxLength: 200, nullable: true),
                    Quantidade = table.Column<int>(nullable: false),
                    lancheId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoCompraItens", x => x.CarrinhoCompraItemId);
                    table.ForeignKey(
                        name: "FK_CarrinhoCompraItens_Lanches_lancheId",
                        column: x => x.lancheId,
                        principalTable: "Lanches",
                        principalColumn: "lancheId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoCompraItens_lancheId",
                table: "CarrinhoCompraItens",
                column: "lancheId");

            migrationBuilder.CreateIndex(
                name: "IX_Lanches_CategoriaId",
                table: "Lanches",
                column: "CategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrinhoCompraItens");

            migrationBuilder.DropTable(
                name: "Lanches");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
