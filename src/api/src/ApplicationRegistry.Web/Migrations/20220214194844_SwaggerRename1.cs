using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationRegistry.Web.Migrations
{
    public partial class SwaggerRename1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationVersionSpecification_SpecificationTypes_SpecificationType",
                table: "ApplicationVersionSpecification");

            migrationBuilder.DropTable(
                name: "SpecificationTypes");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationVersionSpecification_SpecificationType",
                table: "ApplicationVersionSpecification");

            migrationBuilder.DropColumn(
                name: "SpecificationType",
                table: "ApplicationVersionSpecification");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SpecificationType",
                table: "ApplicationVersionSpecification",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SpecificationTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "getutcdate()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificationTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "SpecificationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { "Swagger", "Swagger" });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationVersionSpecification_SpecificationType",
                table: "ApplicationVersionSpecification",
                column: "SpecificationType");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationVersionSpecification_SpecificationTypes_SpecificationType",
                table: "ApplicationVersionSpecification",
                column: "SpecificationType",
                principalTable: "SpecificationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
