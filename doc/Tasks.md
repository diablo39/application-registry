- retention for application versions - configured per project
- support for "unknown" dependecies

docker build -f src\ApplicationRegistry.Web\Dockerfile .

dotnet ef migrations add PackagesTables --context ApplicationRegistryDatabaseContext

            migrationBuilder.DropColumn
                (name: "RowVersion",
                table: "SwaggerSpecificationOperation");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "SwaggerSpecificationOperation",
                rowVersion: true,
                nullable: true);




            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "SwaggerSpecificationOperation",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "SwaggerSpecificationOperation",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");



                dotnet ef migrations  --context ApplicationRegistryDatabaseContext --verbose

dotnet ef migrations add --context ApplicationRegistryDatabaseContext Applications_Code_Unique