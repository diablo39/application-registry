using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Domain.Entities;
using ApplicationRegistry.Domain.Entities.Applications;
using ApplicationRegistry.Domain.Entities.Network;
using ApplicationRegistry.Domain.Entities.Redis;
using ApplicationRegistry.Domain.Persistency;
using ApplicationRegistry.Domain.Repositories.Network;
using ApplicationRegistry.Infrastructure.Domain.Persistency;
using ApplicationRegistry.Infrastructure.Properties;
using ApplicationRegistry.Infrastructure.Repositories.Network;
using Microsoft.EntityFrameworkCore;
using System;

namespace ApplicationRegistry.Database
{
    public class ApplicationRegistryDatabaseContext : DbContext, IUnitOfWork, IQueryDataModel
    {
        private readonly DbContextOptions<ApplicationRegistryDatabaseContext> _options;

        public DbSet<ApplicationEntity> Applications { get; set; }

        public DbSet<ApplicationVersionDependencyEntity> ApplicationVersionDependencies { get; set; }

        public DbSet<ApplicationVersionEntity> ApplicationVersions { get; set; }

        public DbSet<ApplicationVersionSpecificationEntity> ApplicationVersionSpecifications { get; set; }

        public DbSet<ApplicationVersionSpecificationTextEntity> ApplicationVersionSpecificationTexts { get; set; }

        public DbSet<CollectorKnowledgeBaseEntity> CollectorKnowledgeBase { get; set; }

        public DbSet<DependencyEntity> Dependencies { get; set; }

        public DbSet<DependencyVersionEntity> DependencyVersions { get; set; }

        public DbSet<EnvironmentEntity> Environments { get; set; }

        public DbSet<ProjectEntity> Projects { get; set; }

        public DbSet<SwaggerSpecificationOperationEntity> SwaggerSpecificationOperations { get; set; }

        public DbSet<NugetPackageEntity> NugetPackages { get; set; }

        public IApplicationsRepository ApplicationsRepository => new ApplicationsRepository(this);

        public IEnvironmentRepository EnvironmentsRepository => new EnvironmentRepository(this);

        public IProjectsRepository ProjectsRepository => new ProjectsRepository(this);

        public INugetPackageRepository NugetPackageRepository => new NugetPackageRepository(this);

        public DbSet<ApplicationVersionNugetPackageDependency> ApplicationVersionNugetDependencies { get; set; }

        public DbSet<EndpointDependencies> EndpointDependencies { get; set; }
        public DbSet<CollectorLogEntity> CollectorLogs { get; set; }

        public DbSet<RedisEntity> Redis { get; set; }

        public DbSet<ApplicationEndpointEntity> ApplicationEndpoints { get; set; }

        public DbSet<RedisDeploymentTypeEntity> RedisDeploymentTypes { get; set; }

        public DbSet<LoadBalancerEntity> LoadBalancers { get; set; }

        public ILoadBalancerRepository LoadBalancerRepository => new LoadBalancerRepository(this);

        public ApplicationRegistryDatabaseContext() { }

        public ApplicationRegistryDatabaseContext(DbContextOptions<ApplicationRegistryDatabaseContext> options)
            : base(options)
        {
            _options = options;
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
        }
    }
}
