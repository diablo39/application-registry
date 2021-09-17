using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationRegistry.Web.Migrations
{
    public partial class CodeFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DependencyType",
                keyColumn: "Id",
                keyValue: "NPM");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "SwaggerSpecificationOperation");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "SpecificationTypes");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Environment");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "DependencyVersion");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "DependencyType");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Dependency");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CollectorKnowledgeBase");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ApplicationVersionSpecificationText");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ApplicationVersionSpecification");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ApplicationVersionDependency");

            migrationBuilder.DropColumn(
                name: "DependencyFillectionFailed",
                table: "ApplicationVersion");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ApplicationVersion");

            migrationBuilder.DropColumn(
                name: "SpecificationGenerationFailed",
                table: "ApplicationVersion");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Application");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "SwaggerSpecificationOperation",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Application",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "DependencyType",
                keyColumn: "Id",
                keyValue: "MESSAGEBROKERCONSUMER",
                column: "CanBeAddedManualy",
                value: true);

            migrationBuilder.CreateIndex(
                name: "IX_Application_Code",
                table: "Application",
                column: "Code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Application_Code",
                table: "Application");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Task",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "SwaggerSpecificationOperation",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "SwaggerSpecificationOperation",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "SpecificationTypes",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Projects",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Environment",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "DependencyVersion",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "DependencyType",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Dependency",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CollectorKnowledgeBase",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ApplicationVersionSpecificationText",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ApplicationVersionSpecification",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ApplicationVersionDependency",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DependencyFillectionFailed",
                table: "ApplicationVersion",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ApplicationVersion",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SpecificationGenerationFailed",
                table: "ApplicationVersion",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Application",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Application",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "DependencyType",
                keyColumn: "Id",
                keyValue: "MESSAGEBROKERCONSUMER",
                column: "CanBeAddedManualy",
                value: false);

            migrationBuilder.InsertData(
                table: "DependencyType",
                columns: new[] { "Id", "CanBeAddedManualy", "Name" },
                values: new object[] { "NPM", false, "npm" });
        }
    }
}
