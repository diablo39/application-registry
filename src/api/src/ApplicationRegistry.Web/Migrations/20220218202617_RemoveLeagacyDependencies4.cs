using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationRegistry.Web.Migrations
{
    public partial class RemoveLeagacyDependencies4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationVersionDependency");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationVersionDependency",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdApplicationVersion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "getutcdate()"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdDependency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdDependencyVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdParent = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationVersionDependency", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationVersionDependency_ApplicationVersion_IdApplicationVersion",
                        column: x => x.IdApplicationVersion,
                        principalTable: "ApplicationVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationVersionDependency_IdApplicationVersion",
                table: "ApplicationVersionDependency",
                column: "IdApplicationVersion");
        }
    }
}
