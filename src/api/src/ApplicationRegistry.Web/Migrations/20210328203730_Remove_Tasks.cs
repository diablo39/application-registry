using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationRegistry.Web.Migrations
{
    public partial class Remove_Tasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Task");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TaskType = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AutorestClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdApplication = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdApplicationVersionDependency = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdDependency = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_ApplicationVersionDependency_IdApplicationVersionDependency",
                        column: x => x.IdApplicationVersionDependency,
                        principalTable: "ApplicationVersionDependency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Task_Dependency_IdDependency",
                        column: x => x.IdDependency,
                        principalTable: "Dependency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Task_IdApplicationVersionDependency",
                table: "Task",
                column: "IdApplicationVersionDependency");

            migrationBuilder.CreateIndex(
                name: "IX_Task_IdDependency",
                table: "Task",
                column: "IdDependency");
        }
    }
}
