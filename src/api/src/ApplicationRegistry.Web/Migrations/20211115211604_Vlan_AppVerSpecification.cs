using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationRegistry.Web.Migrations
{
    public partial class Vlan_AppVerSpecification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVirtualDirectory",
                table: "Vlan",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Specification",
                table: "ApplicationVersionSpecification",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.Sql("update ApplicationVersionSpecification set ApplicationVersionSpecification.Specification = t2.Specification from ApplicationVersionSpecification t1 inner join ApplicationVersionSpecificationText t2 on t1.id = t2.id");

            migrationBuilder.DropTable(
                name: "ApplicationVersionSpecificationText");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVirtualDirectory",
                table: "Vlan");

            migrationBuilder.DropColumn(
                name: "Specification",
                table: "ApplicationVersionSpecification");

            migrationBuilder.CreateTable(
                name: "ApplicationVersionSpecificationText",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationVersionSpecificationText", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationVersionSpecificationText_ApplicationVersionSpecification_Id",
                        column: x => x.Id,
                        principalTable: "ApplicationVersionSpecification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
