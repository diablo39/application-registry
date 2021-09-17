using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationRegistry.Web.Migrations
{
    public partial class Framework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FrameworkVersion",
                table: "ApplicationVersion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Framework",
                table: "Application",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FrameworkVersion",
                table: "ApplicationVersion");

            migrationBuilder.DropColumn(
                name: "Framework",
                table: "Application");
        }
    }
}
