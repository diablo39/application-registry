using ApplicationRegistry.Domain.Entities.Applications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Infrastructure.Domain.EntityConfigurators
{
    class ApplicationVersionNugetPackageDependencyConfiguration : IEntityTypeConfiguration<ApplicationVersionNugetPackageDependency>
    {
        public void Configure(EntityTypeBuilder<ApplicationVersionNugetPackageDependency> builder)
        {
            builder.ToTable("ApplicationVersionNugetPackageDependency");

            builder.HasKey(m => new { m.IdApplicationVersion, m.IdNugetPackage });

            builder.HasOne(e => e.ApplicationVersion).WithMany(m => m.ApplicationVersionNugetPackageDependencies).HasForeignKey(e => e.IdApplicationVersion);
            builder.HasOne(e => e.NugetPackage).WithMany(m=> m.ApplicationVersions).HasForeignKey(e => e.IdNugetPackage);
        }
    }
}
