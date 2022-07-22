using Microsoft.EntityFrameworkCore.Migrations;

namespace CPMS_AUTO.Migrations
{
    public partial class a5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rank",
                table: "logins",
                newName: "RankName");

            migrationBuilder.AddColumn<int>(
                name: "RankId",
                table: "logins",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RankId",
                table: "logins");

            migrationBuilder.RenameColumn(
                name: "RankName",
                table: "logins",
                newName: "Rank");
        }
    }
}
