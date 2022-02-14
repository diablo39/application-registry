using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Domain.Entities;
using ApplicationRegistry.Domain.Entities.Applications;
using ApplicationRegistry.Domain.Entities.Network;
using ApplicationRegistry.Domain.Entities.Redis;
using ApplicationRegistry.Domain.Persistency;
using ApplicationRegistry.Domain.Repositories;
using ApplicationRegistry.Domain.Repositories.Network;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationRegistry.Database
{
    public interface IQueryDataModel
    {
        DbSet<ApplicationEntity> Applications { get; }

        DbSet<ApplicationEndpointEntity> ApplicationEndpoints { get; }

        DbSet<ApplicationVersionDependencyEntity> ApplicationVersionDependencies { get; }

        DbSet<ApplicationVersionEntity> ApplicationVersions { get; }

        DbSet<ApplicationVersionSwaggerSpecificationEntity> ApplicationVersionSwaggerSpecifications { get; }

        DbSet<CollectorKnowledgeBaseEntity> CollectorKnowledgeBase { get; }

        DbSet<DependencyEntity> Dependencies { get; }

        DbSet<DependencyVersionEntity> DependencyVersions { get; }

        DbSet<EnvironmentEntity> Environments { get; }

        DbSet<SystemEntity> Systems { get; }

        DbSet<SwaggerSpecificationOperationEntity> SwaggerSpecificationOperations { get; }

        DbSet<EndpointDependencies> EndpointDependencies { get; }

        DbSet<CollectorLogEntity> CollectorLogs { get; }

        DbSet<ApplicationVersionNugetPackageDependency> ApplicationVersionNugetDependencies { get; }


        #region Redis

        DbSet<RedisDeploymentTypeEntity> RedisDeploymentTypes { get; }

        DbSet<RedisEntity> Redis { get; }

        #endregion

        #region Nuget

        DbSet<NugetPackageEntity> NugetPackages { get; }

        #endregion

        #region Network
        DbSet<LoadBalancerEntity> LoadBalancers { get; }
        DbSet<VlanEntity> Vlans { get; }

        #endregion
    }

    public interface IUnitOfWork
    {
        #region Leagacy DBSets
        DbSet<CollectorLogEntity> CollectorLogs { get; }

        DbSet<ApplicationVersionSwaggerSpecificationEntity> ApplicationVersionSwaggerSpecifications { get; }

        DbSet<DependencyEntity> Dependencies { get; }

        DbSet<DependencyVersionEntity> DependencyVersions { get; }

        DbSet<ApplicationVersionEntity> ApplicationVersions { get; }

        DbSet<CollectorKnowledgeBaseEntity> CollectorKnowledgeBase { get; }

        DbSet<ApplicationVersionDependencyEntity> ApplicationVersionDependencies { get; }

        DbSet<ApplicationEntity> Applications { get; }

        DbSet<SwaggerSpecificationOperationEntity> SwaggerSpecificationOperations { get; }

        #endregion
        IApplicationsRepository ApplicationsRepository { get; }

        INugetPackageRepository NugetPackageRepository { get; }

        IEnvironmentRepository EnvironmentsRepository { get; }

        ISystemsRepository SystemsRepository { get; }

        ILoadBalancerRepository LoadBalancerRepository { get; }

        IVlanRepository VlanRepository { get; }

        IRedisRepository RedisRepository { get; }

        void FixApplicationVersion(Guid applicationId);

        EntityEntry Add(object entity);
        //
        // Summary:
        //     Saves all changes made in this context to the database.
        //
        // Returns:
        //     The number of state entries written to the database.
        //
        // Exceptions:
        //   T:Microsoft.EntityFrameworkCore.DbUpdateException:
        //     An error is encountered while saving to the database.
        //
        //   T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException:
        //     A concurrency violation is encountered while saving to the database. A concurrency
        //     violation occurs when an unexpected number of rows are affected during save.
        //     This is usually because the data in the database has been modified since it was
        //     loaded into memory.
        //
        // Remarks:
        //     This method will automatically call Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges
        //     to discover any changes to entity instances before saving to the underlying database.
        //     This can be disabled via Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled.
        int SaveChanges();

        /// <summary>
        ///     Asynchronously saves all changes made in this context to the database.
        /// </summary>
        /// <param name="cancellationToken">
        ///     A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
        /// <returns>
        ///     A task that represents the asynchronous save operation. The task result contains
        ///     the number of state entries written to the database.
        /// </returns>
        /// <remarks>
        ///     This method will automatically call Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges
        ///     to discover any changes to entity instances before saving to the underlying database.
        ///     This can be disabled via Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled.
        ///     Multiple active operations on the same context instance are not supported. Use
        ///     'await' to ensure that any asynchronous operations have completed before calling
        ///     another method on this context.
        /// </remarks>
        /// <exception cref="Microsoft.EntityFrameworkCore.DbUpdateException">
        ///     An error is encountered while saving to the database
        /// </exception>
        /// <exception cref="Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException">
        ///     A concurrency violation is encountered while saving to the database. A concurrency
        ///     violation occurs when an unexpected number of rows are affected during save.
        ///     This is usually because the data in the database has been modified since it was
        ///     loaded into memory.
        /// </exception>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}