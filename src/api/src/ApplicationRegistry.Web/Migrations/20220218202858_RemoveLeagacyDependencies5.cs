using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationRegistry.Web.Migrations
{
    public partial class RemoveLeagacyDependencies5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DependencyType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DependencyType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CanBeAddedManualy = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "getutcdate()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DependencyType", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DependencyType",
                columns: new[] { "Id", "CanBeAddedManualy", "Name" },
                values: new object[] { "APPLICATION", true, "Application" });
        }
    }
}
