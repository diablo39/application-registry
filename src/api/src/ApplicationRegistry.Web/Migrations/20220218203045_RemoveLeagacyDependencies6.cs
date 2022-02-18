using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationRegistry.Web.Migrations
{
    public partial class RemoveLeagacyDependencies6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectorKnowledgeBase");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollectorKnowledgeBase",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "getutcdate()"),
                    IdApplicationClient = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdApplicationServer = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdApplicationVersionDependencyChild = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdApplicationVersionDependencyParent = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectorKnowledgeBase", x => x.Id);
                });
        }
    }
}
