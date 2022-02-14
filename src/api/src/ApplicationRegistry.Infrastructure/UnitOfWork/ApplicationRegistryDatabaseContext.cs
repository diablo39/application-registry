using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Domain.Entities;
using ApplicationRegistry.Domain.Entities.Applications;
using ApplicationRegistry.Domain.Entities.Network;
using ApplicationRegistry.Domain.Entities.Redis;
using ApplicationRegistry.Infrastructure.Properties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace ApplicationRegistry.Infrastructure.UnitOfWork
{
    public partial class ApplicationRegistryDatabaseContext : DbContext, IUnitOfWork, IQueryDataModel
    {
        public DbSet<ApplicationEntity> Applications { get; set; }

        public DbSet<ApplicationVersionDependencyEntity> ApplicationVersionDependencies { get; set; }

        public DbSet<ApplicationVersionEntity> ApplicationVersions { get; set; }

        public DbSet<SwaggerApplicationVersionSpecificationEntity> ApplicationVersionSpecifications { get; set; }

        public DbSet<CollectorKnowledgeBaseEntity> CollectorKnowledgeBase { get; set; }

        public DbSet<DependencyEntity> Dependencies { get; set; }

        public DbSet<DependencyVersionEntity> DependencyVersions { get; set; }

        public DbSet<EnvironmentEntity> Environments { get; set; }

        public DbSet<SystemEntity> Systems { get; set; }

        public DbSet<SwaggerSpecificationOperationEntity> SwaggerSpecificationOperations { get; set; }

        public DbSet<NugetPackageEntity> NugetPackages { get; set; }

        public DbSet<ApplicationVersionNugetPackageDependency> ApplicationVersionNugetDependencies { get; set; }

        public DbSet<EndpointDependencies> EndpointDependencies { get; set; }

        public DbSet<CollectorLogEntity> CollectorLogs { get; set; }

        public DbSet<RedisEntity> Redis { get; set; }

        public DbSet<ApplicationEndpointEntity> ApplicationEndpoints { get; set; }

        public DbSet<RedisDeploymentTypeEntity> RedisDeploymentTypes { get; set; }

        public DbSet<LoadBalancerEntity> LoadBalancers { get; set; }

        public DbSet<VlanEntity> Vlans { get; set; }

        public ApplicationRegistryDatabaseContext() { }

        public ApplicationRegistryDatabaseContext(DbContextOptions<ApplicationRegistryDatabaseContext> options)
            : base(options)
        {

        }

        public void FixApplicationVersion(Guid applicationId)
        {
            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory") return;

            var commandText = Resources.MarkPreviousVersionsAsArchived;
            var connection = this.Database.GetDbConnection();

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            using (var command = connection.CreateCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
#pragma warning disable CA2100 // Review SQL queries for security vulnerabilities
                command.CommandText = commandText;
#pragma warning restore CA2100 // Review SQL queries for security vulnerabilities
                command.ExecuteNonQuery();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationRegistryDatabaseContext).Assembly);

            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
                v => v.HasValue ? v.Value.ToUniversalTime() : v,
                v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.IsKeyless)
                {
                    continue;
                }

                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime))
                    {
                        property.SetValueConverter(dateTimeConverter);
                    }
                    else if (property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(nullableDateTimeConverter);
                    }
                }
            }
        }
    }
}
