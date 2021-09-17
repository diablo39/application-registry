using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationRegistry.Web.Migrations
{
    public partial class Redis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "BulkLoad");

            migrationBuilder.CreateTable(
                name: "RedisDeploymentTypes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedisDeploymentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Redis",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Name = table.Column<string>(nullable: true),
                    RedisDeploymentTypeId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Requester = table.Column<string>(nullable: true),
                    RfcId = table.Column<string>(nullable: true),
                    SentinelRedisId = table.Column<Guid>(nullable: true),
                    GroupId = table.Column<string>(nullable: true),
                    NetworkZone = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Redis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Redis_RedisDeploymentTypes_RedisDeploymentTypeId",
                        column: x => x.RedisDeploymentTypeId,
                        principalTable: "RedisDeploymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Redis",
                schema: "BulkLoad",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Name = table.Column<string>(nullable: true),
                    RedisDeploymentTypeId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Requester = table.Column<string>(nullable: true),
                    RfcId = table.Column<string>(nullable: true),
                    SentinelRedisId = table.Column<Guid>(nullable: true),
                    GroupId = table.Column<string>(nullable: true),
                    NetworkZone = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Redis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Redis_RedisDeploymentTypes_RedisDeploymentTypeId",
                        column: x => x.RedisDeploymentTypeId,
                        principalTable: "RedisDeploymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RedisEndpoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RedisId = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Host = table.Column<string>(nullable: true),
                    Port = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedisEndpoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RedisEndpoints_Redis_RedisId",
                        column: x => x.RedisId,
                        principalTable: "Redis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RedisEndpoints",
                schema: "BulkLoad",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RedisId = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Host = table.Column<string>(nullable: true),
                    Port = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedisEndpoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RedisEndpoints_Redis_RedisId",
                        column: x => x.RedisId,
                        principalSchema: "BulkLoad",
                        principalTable: "Redis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RedisDeploymentTypes",
                columns: new[] { "Id", "Description" },
                values: new object[] { "SENTINEL", "Sentinel" });

            migrationBuilder.InsertData(
                table: "RedisDeploymentTypes",
                columns: new[] { "Id", "Description" },
                values: new object[] { "MASTER_SLAVE", "Master-Slave" });

            migrationBuilder.InsertData(
                table: "RedisDeploymentTypes",
                columns: new[] { "Id", "Description" },
                values: new object[] { "CLUSTER", "Cluster" });

            migrationBuilder.CreateIndex(
                name: "IX_Redis_RedisDeploymentTypeId",
                table: "Redis",
                column: "RedisDeploymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RedisEndpoints_RedisId",
                table: "RedisEndpoints",
                column: "RedisId");

            migrationBuilder.CreateIndex(
                name: "IX_Redis_RedisDeploymentTypeId",
                schema: "BulkLoad",
                table: "Redis",
                column: "RedisDeploymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RedisEndpoints_RedisId",
                schema: "BulkLoad",
                table: "RedisEndpoints",
                column: "RedisId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RedisEndpoints");

            migrationBuilder.DropTable(
                name: "RedisEndpoints",
                schema: "BulkLoad");

            migrationBuilder.DropTable(
                name: "Redis");

            migrationBuilder.DropTable(
                name: "Redis",
                schema: "BulkLoad");

            migrationBuilder.DropTable(
                name: "RedisDeploymentTypes");
        }
    }
}
