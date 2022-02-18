using ApplicationRegistry.CQRS.Abstraction;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationRegistry.Application.Commands
{
    public class CollectCliResultCommand : ICommand
    {
        public class ApplicationVersionDependency
        {
            [Required]
            public string Name { get; set; }

            [Required]
            public string DependencyType { get; set; }

            [Required]
            public string Version { get; set; }

            public Dictionary<string, object> VersionExtraProperties { get; set; }

            public Dictionary<string, object> ExtraProperties { get; set; }
        }

        public class ApplicationVersionSpecification
        {
            public string ContentType { get; set; }

            [Required]
            public string Specification { get; set; }

            public string SpecificationType { get; set; }

            [Required]
            public string Code { get; set; }
        }

        [Required]
        public string ApplicationCode { get; set; }

        [Required]
        public string IdEnvironment { get; set; }

        public string Framework { get; set; }

        public string FrameworkVersion { get; set; }

        public string Version { get; set; }

        public string IdCommit { get; set; }

        public string ToolsVersion { get; set; }

        public string RepositoryUrl { get; set; }

        public bool ExecutionSucceeded { get; set; }

        public string ExecutionDuration { get; set; }

        public Dictionary<string, bool> BatchStatuses { get; set; }

        // Navigation properties

        public List<ApplicationVersionSpecification> Specifications { get; set; }

        public List<ApplicationVersionDependency> Dependencies { get; set; }
    }

    public class CollectCliResultCommandValidator : AbstractValidator<CollectCliResultCommand>
    {
        public CollectCliResultCommandValidator()
        {
            RuleFor(e => e.Version).NotEmpty();
        }
    }


}
