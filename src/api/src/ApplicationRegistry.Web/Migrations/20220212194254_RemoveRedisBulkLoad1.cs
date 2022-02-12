using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationRegistry.Web.Migrations
{
    public partial class RemoveRedisBulkLoad1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Redis_Environment_IdEnvironment",
                table: "Redis");

            migrationBuilder.DropForeignKey(
                name: "FK_Redis_RedisDeploymentTypes_RedisDeploymentTypeId",
                table: "Redis");

            migrationBuilder.DropIndex(
                name: "IX_Redis_IdEnvironment",
                schema: "BulkLoad",
                table: "Redis");

            migrationBuilder.DropIndex(
                name: "IX_Redis_RedisDeploymentTypeId",
                schema: "BulkLoad",
                table: "Redis");

            migrationBuilder.RenameIndex(
                name: "IX_Redis_RedisDeploymentTypeId1",
                table: "Redis",
                newName: "IX_Redis_RedisDeploymentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Redis_IdEnvironment1",
                table: "Redis",
                newName: "IX_Redis_IdEnvironment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Redis_RedisDeploymentTypeId",
                table: "Redis",
                newName: "IX_Redis_RedisDeploymentTypeId1");

            migrationBuilder.RenameIndex(
                name: "IX_Redis_IdEnvironment",
                table: "Redis",
                newName: "IX_Redis_IdEnvironment1");

            migrationBuilder.CreateIndex(
                name: "IX_Redis_IdEnvironment",
                schema: "BulkLoad",
                table: "Redis",
                column: "IdEnvironment");

            migrationBuilder.CreateIndex(
                name: "IX_Redis_RedisDeploymentTypeId",
                schema: "BulkLoad",
                table: "Redis",
                column: "RedisDeploymentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Redis_Environment_IdEnvironment",
                table: "Redis",
                column: "IdEnvironment",
                principalTable: "Environment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Redis_RedisDeploymentTypes_RedisDeploymentTypeId",
                table: "Redis",
                column: "RedisDeploymentTypeId",
                principalTable: "RedisDeploymentTypes",
                principalColumn: "Id");
        }
    }
}
