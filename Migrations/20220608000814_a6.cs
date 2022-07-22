using Microsoft.EntityFrameworkCore.Migrations;

namespace CPMS_AUTO.Migrations
{
    public partial class a6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RankName",
                table: "logins");

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RankName",
                table: "logins",
                type: "nvarchar(max)",
                nullable: true);

           
        }
    }
}
