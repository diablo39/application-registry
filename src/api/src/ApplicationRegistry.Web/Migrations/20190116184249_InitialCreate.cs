using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationRegistry.Web.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollectorKnowledgeBase",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    ClientType = table.Column<string>(nullable: true),
                    IdApplicationServer = table.Column<Guid>(nullable: false),
                    IdApplicationClient = table.Column<Guid>(nullable: false),
                    IdApplicationVersionDependencyParent = table.Column<Guid>(nullable: false),
                    IdApplicationVersionDependencyChild = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectorKnowledgeBase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DependencyType",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Name = table.Column<string>(nullable: false),
                    CanBeAddedManualy = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DependencyType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Environment",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 25, nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Environment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpecificationTypes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    TaskType = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    IdApplicationVersionDependency = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dependency",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Name = table.Column<string>(nullable: false),
                    IdDependencyType = table.Column<string>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    IdApplication = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependency", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dependency_DependencyType_IdDependencyType",
                        column: x => x.IdDependencyType,
                        principalTable: "DependencyType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Description = table.Column<string>(nullable: true),
                    Owner = table.Column<string>(nullable: true),
                    RepositoryUrl = table.Column<string>(nullable: true),
                    BuildProcessUrls = table.Column<string>(nullable: true),
                    IdProject = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Application_Projects_IdProject",
                        column: x => x.IdProject,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DependencyVersion",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    RowVersion = table.Column<byte[]>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IdDependency = table.Column<string>(nullable: false),
                    Version = table.Column<string>(nullable: false),
                    DependencyId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DependencyVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DependencyVersion_Dependency_DependencyId",
                        column: x => x.DependencyId,
                        principalTable: "Dependency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationVersion",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdApplication = table.Column<Guid>(nullable: false),
                    IdEnvironment = table.Column<string>(nullable: false),
                    Version = table.Column<string>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    IdCommit = table.Column<string>(nullable: true),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationVersion_Application_IdApplication",
                        column: x => x.IdApplication,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationVersion_Environment_IdEnvironment",
                        column: x => x.IdEnvironment,
                        principalTable: "Environment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationVersionDependency",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    IdDependency = table.Column<string>(nullable: false),
                    IdDependencyVersion = table.Column<string>(nullable: true),
                    ExtraProperties = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    IdApplicationVersion = table.Column<Guid>(nullable: false),
                    IdParent = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationVersionDependency", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationVersionDependency_ApplicationVersion_IdApplicationVersion",
                        column: x => x.IdApplicationVersion,
                        principalTable: "ApplicationVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationVersionDependency_Dependency_IdDependency",
                        column: x => x.IdDependency,
                        principalTable: "Dependency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationVersionDependency_DependencyVersion_IdDependencyVersion",
                        column: x => x.IdDependencyVersion,
                        principalTable: "DependencyVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationVersionSpecification",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdApplicationVersion = table.Column<Guid>(nullable: false),
                    ContentType = table.Column<string>(nullable: true),
                    SpecificationType = table.Column<string>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    SpecificationTextHash = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    OperationsStringified = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationVersionSpecification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationVersionSpecification_ApplicationVersion_IdApplicationVersion",
                        column: x => x.IdApplicationVersion,
                        principalTable: "ApplicationVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationVersionSpecification_SpecificationTypes_SpecificationType",
                        column: x => x.SpecificationType,
                        principalTable: "SpecificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationVersionSpecificationText",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Specification = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "SwaggerSpecificationOperation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IdApplicationVersionSpecification = table.Column<Guid>(nullable: false),
                    Path = table.Column<string>(nullable: true),
                    OperationId = table.Column<string>(nullable: true),
                    HttpMethod = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SwaggerSpecificationOperation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SwaggerSpecificationOperation_ApplicationVersionSpecification_IdApplicationVersionSpecification",
                        column: x => x.IdApplicationVersionSpecification,
                        principalTable: "ApplicationVersionSpecification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DependencyType",
                columns: new[] { "Id", "CanBeAddedManualy", "CreateDate", "Name", "RowVersion" },
                values: new object[,]
                {
                    { "NUGET", false, new DateTime(2019, 1, 16, 19, 42, 49, 380, DateTimeKind.Local), "Nuget", null },
                    { "APPLICATION", true, new DateTime(2019, 1, 16, 19, 42, 49, 385, DateTimeKind.Local), "Application", null },
                    { "EXTERNALAPPLICATION", false, new DateTime(2019, 1, 16, 19, 42, 49, 385, DateTimeKind.Local), "External Application", null },
                    { "FRAMEWORK", false, new DateTime(2019, 1, 16, 19, 42, 49, 385, DateTimeKind.Local), "Framework", null },
                    { "NPM", false, new DateTime(2019, 1, 16, 19, 42, 49, 385, DateTimeKind.Local), "npm", null },
                    { "DATABASE", false, new DateTime(2019, 1, 16, 19, 42, 49, 385, DateTimeKind.Local), "Database", null },
                    { "AUTORESTCLIENT", false, new DateTime(2019, 1, 16, 19, 42, 49, 385, DateTimeKind.Local), "AutoRest Client", null }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CreateDate", "Description", "Name", "RowVersion" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2019, 1, 16, 19, 42, 49, 386, DateTimeKind.Local), "Project for applications generated by CLI", "Unasigned applications", null });

            migrationBuilder.InsertData(
                table: "SpecificationTypes",
                columns: new[] { "Id", "CreateDate", "Name", "RowVersion" },
                values: new object[] { "Swagger", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Swagger", null });

            migrationBuilder.CreateIndex(
                name: "IX_Application_IdProject",
                table: "Application",
                column: "IdProject");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationVersion_IdApplication",
                table: "ApplicationVersion",
                column: "IdApplication");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationVersion_IdEnvironment",
                table: "ApplicationVersion",
                column: "IdEnvironment");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationVersionDependency_IdApplicationVersion",
                table: "ApplicationVersionDependency",
                column: "IdApplicationVersion");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationVersionDependency_IdDependency",
                table: "ApplicationVersionDependency",
                column: "IdDependency");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationVersionDependency_IdDependencyVersion",
                table: "ApplicationVersionDependency",
                column: "IdDependencyVersion");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationVersionSpecification_IdApplicationVersion",
                table: "ApplicationVersionSpecification",
                column: "IdApplicationVersion");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationVersionSpecification_SpecificationType",
                table: "ApplicationVersionSpecification",
                column: "SpecificationType");

            migrationBuilder.CreateIndex(
                name: "IX_Dependency_IdDependencyType",
                table: "Dependency",
                column: "IdDependencyType");

            migrationBuilder.CreateIndex(
                name: "IX_DependencyVersion_DependencyId",
                table: "DependencyVersion",
                column: "DependencyId");

            migrationBuilder.CreateIndex(
                name: "IX_SwaggerSpecificationOperation_IdApplicationVersionSpecification",
                table: "SwaggerSpecificationOperation",
                column: "IdApplicationVersionSpecification");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationVersionDependency");

            migrationBuilder.DropTable(
                name: "ApplicationVersionSpecificationText");

            migrationBuilder.DropTable(
                name: "CollectorKnowledgeBase");

            migrationBuilder.DropTable(
                name: "SwaggerSpecificationOperation");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "DependencyVersion");

            migrationBuilder.DropTable(
                name: "ApplicationVersionSpecification");

            migrationBuilder.DropTable(
                name: "Dependency");

            migrationBuilder.DropTable(
                name: "ApplicationVersion");

            migrationBuilder.DropTable(
                name: "SpecificationTypes");

            migrationBuilder.DropTable(
                name: "DependencyType");

            migrationBuilder.DropTable(
                name: "Application");

            migrationBuilder.DropTable(
                name: "Environment");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
