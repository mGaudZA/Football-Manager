using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Football_Manager.Migrations
{
    public partial class AddPortraitKeyToPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PortraitKey",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PortraitKey",
                table: "Players");
        }
    }
}
