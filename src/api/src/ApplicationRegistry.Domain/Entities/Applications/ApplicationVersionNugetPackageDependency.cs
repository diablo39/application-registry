using ApplicationRegistry.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Domain.Entities.Applications
{
    public class ApplicationVersionNugetPackageDependency
    {
        public Guid IdApplicationVersion { get; set; }

        public ApplicationVersionEntity ApplicationVersion { get; set; }

        public Int64 IdNugetPackage { get; set; }

        public NugetPackageEntity NugetPackage { get; set; }
    }
}
