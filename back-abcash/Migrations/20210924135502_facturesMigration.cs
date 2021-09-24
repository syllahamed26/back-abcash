using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace back_abcash.Migrations
{
    public partial class facturesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Factures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "1, 1"),
                    Numfact = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Refcontrini = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Numavenant = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Refpds = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Etat = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    DateCalcul = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Debut = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Fin = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Abontht = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Consoht = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Prestht = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Montantttc = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factures", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Factures");
        }
    }
}
