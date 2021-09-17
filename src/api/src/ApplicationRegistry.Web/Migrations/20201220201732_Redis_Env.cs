using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationRegistry.Web.Migrations
{
    public partial class Redis_Env : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdEnvironment",
                schema: "BulkLoad",
                table: "Redis",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdEnvironment",
                table: "Redis",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Redis_IdEnvironment",
                schema: "BulkLoad",
                table: "Redis",
                column: "IdEnvironment");

            migrationBuilder.CreateIndex(
                name: "IX_Redis_IdEnvironment",
                table: "Redis",
                column: "IdEnvironment");

            migrationBuilder.AddForeignKey(
                name: "FK_Redis_Environment_IdEnvironment",
                table: "Redis",
                column: "IdEnvironment",
                principalTable: "Environment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Redis_Environment_IdEnvironment",
                schema: "BulkLoad",
                table: "Redis",
                column: "IdEnvironment",
                principalTable: "Environment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Redis_Environment_IdEnvironment",
                table: "Redis");

            migrationBuilder.DropForeignKey(
                name: "FK_Redis_Environment_IdEnvironment",
                schema: "BulkLoad",
                table: "Redis");

            migrationBuilder.DropIndex(
                name: "IX_Redis_IdEnvironment",
                schema: "BulkLoad",
                table: "Redis");

            migrationBuilder.DropIndex(
                name: "IX_Redis_IdEnvironment",
                table: "Redis");

            migrationBuilder.DropColumn(
                name: "IdEnvironment",
                schema: "BulkLoad",
                table: "Redis");

            migrationBuilder.DropColumn(
                name: "IdEnvironment",
                table: "Redis");
        }
    }
}
