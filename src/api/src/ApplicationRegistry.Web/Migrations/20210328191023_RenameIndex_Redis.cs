using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationRegistry.Web.Migrations
{
    public partial class RenameIndex_Redis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_RedisEndpoints_RedisId",
                table: "RedisEndpoints",
                newName: "IX_RedisEndpoints_RedisId1");

            migrationBuilder.RenameIndex(
                name: "IX_Redis_RedisDeploymentTypeId",
                table: "Redis",
                newName: "IX_Redis_RedisDeploymentTypeId1");

            migrationBuilder.RenameIndex(
                name: "IX_Redis_IdEnvironment",
                table: "Redis",
                newName: "IX_Redis_IdEnvironment1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_RedisEndpoints_RedisId1",
                table: "RedisEndpoints",
                newName: "IX_RedisEndpoints_RedisId");

            migrationBuilder.RenameIndex(
                name: "IX_Redis_RedisDeploymentTypeId1",
                table: "Redis",
                newName: "IX_Redis_RedisDeploymentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Redis_IdEnvironment1",
                table: "Redis",
                newName: "IX_Redis_IdEnvironment");
        }
    }
}
