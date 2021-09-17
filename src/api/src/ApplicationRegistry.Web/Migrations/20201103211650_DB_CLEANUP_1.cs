using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationRegistry.Web.Migrations
{
    public partial class DB_CLEANUP_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DependencyType",
                keyColumn: "Id",
                keyValue: "DATABASE");

            migrationBuilder.DeleteData(
                table: "DependencyType",
                keyColumn: "Id",
                keyValue: "EXTERNALAPPLICATION");

            migrationBuilder.DeleteData(
                table: "DependencyType",
                keyColumn: "Id",
                keyValue: "FRAMEWORK");

            migrationBuilder.DeleteData(
                table: "DependencyType",
                keyColumn: "Id",
                keyValue: "MESSAGEBROKERCONSUMER");

            migrationBuilder.DeleteData(
                table: "DependencyType",
                keyColumn: "Id",
                keyValue: "MESSAGEBROKERPRODUCER");

            migrationBuilder.DeleteData(
                table: "DependencyType",
                keyColumn: "Id",
                keyValue: "NUGET");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DependencyType",
                columns: new[] { "Id", "CanBeAddedManualy", "Name" },
                values: new object[,]
                {
                    { "NUGET", false, "Nuget" },
                    { "EXTERNALAPPLICATION", false, "External Application" },
                    { "FRAMEWORK", false, "Framework" },
                    { "DATABASE", false, "Database" },
                    { "MESSAGEBROKERCONSUMER", true, "Message Broker Topic Consumer" },
                    { "MESSAGEBROKERPRODUCER", false, "Message Broker Topic Producer" }
                });
        }
    }
}
