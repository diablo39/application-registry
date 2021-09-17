using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationRegistry.Web.Migrations
{
    public partial class NugetPackage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NugetPackage",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Version = table.Column<string>(maxLength: 400, nullable: false),
                    Name = table.Column<string>(maxLength: 400, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NugetPackage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationVersionNugetPackageDependency",
                columns: table => new
                {
                    IdApplicationVersion = table.Column<Guid>(nullable: false),
                    IdNugetPackage = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationVersionNugetPackageDependency", x => new { x.IdApplicationVersion, x.IdNugetPackage });
                    table.ForeignKey(
                        name: "FK_ApplicationVersionNugetPackageDependency_ApplicationVersion_IdApplicationVersion",
                        column: x => x.IdApplicationVersion,
                        principalTable: "ApplicationVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationVersionNugetPackageDependency_NugetPackage_IdNugetPackage",
                        column: x => x.IdNugetPackage,
                        principalTable: "NugetPackage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationVersionNugetPackageDependency_IdNugetPackage",
                table: "ApplicationVersionNugetPackageDependency",
                column: "IdNugetPackage");

            migrationBuilder.CreateIndex(
                name: "IX_NugetPackage_Name_Version",
                table: "NugetPackage",
                columns: new[] { "Name", "Version" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationVersionNugetPackageDependency");

            migrationBuilder.DropTable(
                name: "NugetPackage");
        }
    }
}
