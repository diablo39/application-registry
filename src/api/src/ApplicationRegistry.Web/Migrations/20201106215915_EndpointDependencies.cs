using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationRegistry.Web.Migrations
{
    public partial class EndpointDependencies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EndpointDependency",
                columns: table => new
                {
                    Id = table.Column<HierarchyId>(nullable: false),
                    EnvironmentId = table.Column<string>(nullable: true),
                    ApplicationCode = table.Column<string>(nullable: true),
                    HttpMethod = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    HierarchyChecksum = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndpointDependency", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EndpointDependency_HierarchyChecksum",
                table: "EndpointDependency",
                column: "HierarchyChecksum",
                unique: true,
                filter: "[HierarchyChecksum] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EndpointDependency");
        }
    }
}
