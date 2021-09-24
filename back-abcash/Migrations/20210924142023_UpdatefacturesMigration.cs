using Microsoft.EntityFrameworkCore.Migrations;

namespace back_abcash.Migrations
{
    public partial class UpdatefacturesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Etat",
                table: "Factures",
                type: "NVARCHAR2(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "NUMBER(1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Etat",
                table: "Factures",
                type: "NUMBER(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(1)");
        }
    }
}
