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

        [Required]
        public Guid IdApplication { get; set; }

        [Required]
        public string IdEnvironment { get; set; }

        [Required]
        public string Version { get; set; }

        public DateTime CreateDate { get; set; }

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

        public List<ApplicationVersionSpecificationEntity> Specifications { get; set; }

        public List<ApplicationVersionDependencyEntity> Dependencies { get; set; }

        public List<ApplicationVersionNugetPackageDependency> ApplicationVersionNugetPackageDependencies { get; set; }

        public ApplicationVersionEntity()
        {
            Id = Guid.NewGuid();

            ApplicationVersionNugetPackageDependencies = new List<ApplicationVersionNugetPackageDependency>();
        }
    }
}
