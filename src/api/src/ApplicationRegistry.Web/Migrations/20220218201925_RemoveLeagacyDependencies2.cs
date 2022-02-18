using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationRegistry.Web.Migrations
{
    public partial class RemoveLeagacyDependencies2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationVersionDependency_Dependency_IdDependency",
                table: "ApplicationVersionDependency");

            migrationBuilder.DropForeignKey(
                name: "FK_DependencyVersion_Dependency_DependencyId",
                table: "DependencyVersion");

            migrationBuilder.DropTable(
                name: "Dependency");

            migrationBuilder.DropIndex(
                name: "IX_DependencyVersion_DependencyId",
                table: "DependencyVersion");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationVersionDependency_IdDependency",
                table: "ApplicationVersionDependency");

            migrationBuilder.AlterColumn<string>(
                name: "DependencyId",
                table: "DependencyVersion",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "IdDependency",
                table: "ApplicationVersionDependency",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DependencyId",
                table: "DependencyVersion",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "IdDependency",
                table: "ApplicationVersionDependency",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Dependency",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdDependencyType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "getutcdate()"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependency", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dependency_DependencyType_IdDependencyType",
                        column: x => x.IdDependencyType,
                        principalTable: "DependencyType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DependencyVersion_DependencyId",
                table: "DependencyVersion",
                column: "DependencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationVersionDependency_IdDependency",
                table: "ApplicationVersionDependency",
                column: "IdDependency");

            migrationBuilder.CreateIndex(
                name: "IX_Dependency_IdDependencyType",
                table: "Dependency",
                column: "IdDependencyType");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationVersionDependency_Dependency_IdDependency",
                table: "ApplicationVersionDependency",
                column: "IdDependency",
                principalTable: "Dependency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DependencyVersion_Dependency_DependencyId",
                table: "DependencyVersion",
                column: "DependencyId",
                principalTable: "Dependency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
