using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationRegistry.Web.Migrations
{
    public partial class ApplicationVersion_FailedIndicators : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DependencyFillectionFailed",
                table: "ApplicationVersion",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SpecificationGenerationFailed",
                table: "ApplicationVersion",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DependencyFillectionFailed",
                table: "ApplicationVersion");

            migrationBuilder.DropColumn(
                name: "SpecificationGenerationFailed",
                table: "ApplicationVersion");
        }
    }
}
