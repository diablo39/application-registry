using ApplicationRegistry.Database.EntityConfigurators;
using ApplicationRegistry.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Infrastructure.Domain.EntityConfigurators
{
    class EndpointDependenciesConfiguration : EntityTypeConfigurationBase<EndpointDependencies>
    {
        public override void ConfigureEntity(EntityTypeBuilder<EndpointDependencies> builder)
        {
            builder.ToTable("EndpointDependency");

            builder.HasIndex(u => u.HierarchyChecksum).IsUnique();
        }
    }
}
