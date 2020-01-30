using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class sfdaskmvksmgaskgsdngkasdzgknarsgh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsModerator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsModerator",
                table: "AspNetUsers");
        }
    }
}
