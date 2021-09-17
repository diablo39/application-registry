using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationRegistry.Web.Migrations
{
    public partial class MessageBroker_AddEventTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DependencyType",
                columns: new[] { "Id", "CanBeAddedManualy", "CreateDate", "Name", "RowVersion" },
                values: new object[,]
                {
                    { "MESSAGEBROKERCONSUMER", false, new DateTime(2019, 2, 25, 20, 51, 26, 976, DateTimeKind.Local), "Message Broker Topic Consumer", null },
                    { "MESSAGEBROKERPRODUCER", false, new DateTime(2019, 2, 25, 20, 51, 26, 976, DateTimeKind.Local), "Message Broker Topic Producer", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DependencyType",
                keyColumns: new[] { "Id", "RowVersion" },
                keyValues: new object[] { "MESSAGEBROKERCONSUMER", null });

            migrationBuilder.DeleteData(
                table: "DependencyType",
                keyColumns: new[] { "Id", "RowVersion" },
                keyValues: new object[] { "MESSAGEBROKERPRODUCER", null });
        }
    }
}
