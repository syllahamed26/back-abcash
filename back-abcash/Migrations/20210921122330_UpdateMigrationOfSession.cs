using Microsoft.EntityFrameworkCore.Migrations;

namespace back_abcash.Migrations
{
    public partial class UpdateMigrationOfSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Statut",
                table: "Sessions",
                type: "NUMBER(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Statut",
                table: "Sessions");
        }
    }
}
