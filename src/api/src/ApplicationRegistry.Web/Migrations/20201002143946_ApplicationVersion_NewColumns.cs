using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationRegistry.Web.Migrations
{
    public partial class ApplicationVersion_NewColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CollectorBatchStatuses",
                table: "ApplicationVersion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CollectorExecutionDuration",
                table: "ApplicationVersion",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CollectorExecutionSucceeded",
                table: "ApplicationVersion",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CollectorBatchStatuses",
                table: "ApplicationVersion");

            migrationBuilder.DropColumn(
                name: "CollectorExecutionDuration",
                table: "ApplicationVersion");

            migrationBuilder.DropColumn(
                name: "CollectorExecutionSucceeded",
                table: "ApplicationVersion");
        }
    }
}
