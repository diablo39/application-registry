using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationRegistry.Web.Migrations
{
    public partial class ProjectRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_Projects_IdProject",
                table: "Application");

            migrationBuilder.RenameColumn(
                name: "IdProject",
                table: "Application",
                newName: "IdSystem");

            migrationBuilder.RenameIndex(
                name: "IX_Application_IdProject",
                table: "Application",
                newName: "IX_Application_IdSystem");

            migrationBuilder.DropPrimaryKey("PK_Projects", "Projects");

            migrationBuilder.RenameTable(name: "Projects", newName: "Systems");

            migrationBuilder.AddPrimaryKey("PK_Systems", "Systems", "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Systems_IdSystem",
                table: "Application",
                column: "IdSystem",
                principalTable: "Systems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
