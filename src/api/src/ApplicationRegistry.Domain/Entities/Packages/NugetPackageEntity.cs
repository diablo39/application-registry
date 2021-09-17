using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Domain.Entities.Applications;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationRegistry.Domain.Entities
{
    public class NugetPackageEntity
    {
        public Int64 Id { get; set; }

        [Required]
        public string Version { get; private set; }

        [Required]
        public string Name { get; private set; }

        public DateTime CreateDate { get; set; }

        public List<ApplicationVersionNugetPackageDependency> ApplicationVersions { get; set; }

        public NugetPackageEntity(string name, string version)
        {
            Version = version;
            Name = name;
        }
    }
}
