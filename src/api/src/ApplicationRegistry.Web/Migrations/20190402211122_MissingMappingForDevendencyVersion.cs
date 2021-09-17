using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationRegistry.Web.Migrations
{
    public partial class MissingMappingForDevendencyVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update [dbo].[DependencyVersion] set DependencyId = IdDependency");

            migrationBuilder.DropForeignKey(
                name: "FK_DependencyVersion_Dependency_DependencyId",
                table: "DependencyVersion");

            migrationBuilder.DropColumn(
                name: "IdDependency",
                table: "DependencyVersion");

            migrationBuilder.AlterColumn<int>(
                name: "ValidationStatus",
                table: "DependencyVersion",
                nullable: false,
                defaultValueSql: "(0)",
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "DependencyId",
                table: "DependencyVersion",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "DependencyVersion",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime));

            migrationBuilder.AddForeignKey(
                name: "FK_DependencyVersion_Dependency_DependencyId",
                table: "DependencyVersion",
                column: "DependencyId",
                principalTable: "Dependency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "DependencyVersion");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "DependencyVersion",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           // no go back
        }
    }
}
