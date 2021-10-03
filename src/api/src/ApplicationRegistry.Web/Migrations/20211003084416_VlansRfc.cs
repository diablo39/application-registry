using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationRegistry.Web.Migrations
{
    public partial class VlansRfc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RFC",
                table: "Vlan",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RFC",
                table: "Vlan");
        }
    }
}
