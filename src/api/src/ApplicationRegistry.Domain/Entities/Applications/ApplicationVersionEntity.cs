using ApplicationRegistry.Domain.Entities;
using ApplicationRegistry.Domain.Entities.Applications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApplicationRegistry.Database.Entities
{
    public class ApplicationVersionEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid IdApplication { get; }

        public string IdEnvironment { get; set; }

        public string Version { get; set; }

        public DateTimeOffset CreateDate { get; set; }

        public string IdCommit { get; set; }

        public bool IsArchived { get; set; }

        public string ToolsVersion { get; set; }

        public ValidationStatus ValidationStatus { get; set; }

        public bool CollectorExecutionSucceeded { get; set; }

        public string CollectorExecutionDuration { get; set; }

        public string CollectorBatchStatuses { get; set; }

        public string FrameworkVersion { get; set; }

        // Navigation properties

        public ApplicationEntity Application { get; set; }

        public EnvironmentEntity Environment { get; set; }

        public List<ApplicationVersionSwaggerSpecificationEntity> SwaggerSpecifications { get; set; }

        public List<ApplicationVersionNugetPackageDependency> ApplicationVersionNugetPackageDependencies { get; set; }

        public ApplicationVersionEntity(Guid id, Guid idApplication, string idEnvironment, string version, DateTimeOffset createDate = default)
        {
            Guard.IsNotDefault(id, nameof(id));
            Guard.IsNotDefault(idApplication, nameof(idApplication));
            Guard.IsNotNullOrWhiteSpace(idEnvironment, nameof(idEnvironment));
            Guard.IsNotNullOrWhiteSpace(version, nameof(version));

            Id = id;
            IdApplication = idApplication;
            IdEnvironment = idEnvironment;
            Version = version;
            IsArchived = false;
            CreateDate = createDate == default ? DateTime.UtcNow : createDate;
            ApplicationVersionNugetPackageDependencies = new List<ApplicationVersionNugetPackageDependency>();
        }
    }
}