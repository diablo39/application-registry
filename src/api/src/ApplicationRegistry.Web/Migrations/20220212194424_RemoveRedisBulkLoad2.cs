using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationRegistry.Web.Migrations
{
    public partial class RemoveRedisBulkLoad2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RedisEndpoints_Redis_RedisId",
                schema: "BulkLoad",
                table: "RedisEndpoints");

            migrationBuilder.DropTable(
                name: "Redis",
                schema: "BulkLoad");

            migrationBuilder.DropIndex(
                name: "IX_RedisEndpoints_RedisId",
                schema: "BulkLoad",
                table: "RedisEndpoints");

            migrationBuilder.RenameIndex(
                name: "IX_RedisEndpoints_RedisId1",
                table: "RedisEndpoints",
                newName: "IX_RedisEndpoints_RedisId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_RedisEndpoints_RedisId",
                table: "RedisEndpoints",
                newName: "IX_RedisEndpoints_RedisId1");

            migrationBuilder.CreateTable(
                name: "Redis",
                schema: "BulkLoad",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "getutcdate()"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdEnvironment = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NetworkZone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RedisDeploymentTypeId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Requester = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RfcId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SentinelRedisId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Redis", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RedisEndpoints_RedisId",
                schema: "BulkLoad",
                table: "RedisEndpoints",
                column: "RedisId");

            migrationBuilder.AddForeignKey(
                name: "FK_RedisEndpoints_Redis_RedisId",
                schema: "BulkLoad",
                table: "RedisEndpoints",
                column: "RedisId",
                principalSchema: "BulkLoad",
                principalTable: "Redis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
