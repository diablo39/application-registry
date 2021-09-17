using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationRegistry.Web.Migrations
{
    public partial class ValidationStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ValidationStatus",
                table: "DependencyVersion",
                nullable: false,
                defaultValueSql: "(0)");

            migrationBuilder.AddColumn<int>(
                name: "ValidationStatus",
                table: "ApplicationVersion",
                nullable: false,
                defaultValueSql: "(0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValidationStatus",
                table: "DependencyVersion");

            migrationBuilder.DropColumn(
                name: "ValidationStatus",
                table: "ApplicationVersion");
        }
    }
}
