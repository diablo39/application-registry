using ApplicationRegistry.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using ApplicationRegistry.Database.EntityConfigurators;

namespace ApplicationRegistry.Infrastructure.Domain.EntityConfigurators.Dependencies
{
    class ApplicationVersionDependencyEntityConfiguration: EntityTypeConfigurationBase<ApplicationVersionDependencyEntity>
    {
        public override void ConfigureEntity(EntityTypeBuilder<ApplicationVersionDependencyEntity> builder)
        {
            builder.ToTable("ApplicationVersionDependency");

            builder
                .HasOne(e => e.ApplicationVersion)
                .WithMany(e => e.Dependencies)
                .HasForeignKey(e => e.IdApplicationVersion);

            builder
                .Property(e => e.IdDependency)
                .HasConversion(s => s, s => s.ToLower());

            builder
                .Property(e => e.IdDependencyVersion)
                .HasConversion(s => s, s => s.ToLower());
        }
    }
}
