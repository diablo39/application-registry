using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationRegistry.Web.Migrations
{
    public partial class RemoveLeagacyDependencies3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationVersionDependency_DependencyVersion_IdDependencyVersion",
                table: "ApplicationVersionDependency");

            migrationBuilder.DropTable(
                name: "DependencyVersion");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationVersionDependency_IdDependencyVersion",
                table: "ApplicationVersionDependency");

            migrationBuilder.AlterColumn<string>(
                name: "IdDependencyVersion",
                table: "ApplicationVersionDependency",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdDependencyVersion",
                table: "ApplicationVersionDependency",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DependencyVersion",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "getutcdate()"),
                    DependencyId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValidationStatus = table.Column<int>(type: "int", nullable: false, defaultValueSql: "(0)"),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DependencyVersion", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationVersionDependency_IdDependencyVersion",
                table: "ApplicationVersionDependency",
                column: "IdDependencyVersion");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationVersionDependency_DependencyVersion_IdDependencyVersion",
                table: "ApplicationVersionDependency",
                column: "IdDependencyVersion",
                principalTable: "DependencyVersion",
                principalColumn: "Id");
        }
    }
}
