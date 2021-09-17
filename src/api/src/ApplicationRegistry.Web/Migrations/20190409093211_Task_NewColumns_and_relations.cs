using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationRegistry.Web.Migrations
{
    public partial class Task_NewColumns_and_relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AutorestClientName",
                table: "Task",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdDependency",
                table: "Task",
                nullable: true,
                maxLength: 450);

            migrationBuilder.CreateIndex(
                name: "IX_Task_IdApplicationVersionDependency",
                table: "Task",
                column: "IdApplicationVersionDependency");

            migrationBuilder.CreateIndex(
                name: "IX_Task_IdDependency",
                table: "Task",
                column: "IdDependency");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_ApplicationVersionDependency_IdApplicationVersionDependency",
                table: "Task",
                column: "IdApplicationVersionDependency",
                principalTable: "ApplicationVersionDependency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Dependency_IdDependency",
                table: "Task",
                column: "IdDependency",
                principalTable: "Dependency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_ApplicationVersionDependency_IdApplicationVersionDependency",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_Dependency_IdDependency",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_Task_IdApplicationVersionDependency",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_Task_IdDependency",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "AutorestClientName",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "IdDependency",
                table: "Task");
        }
    }
}
