using ApplicationRegistry.Database.EntityConfigurators;
using ApplicationRegistry.Domain.Entities.Applications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Infrastructure.EntityConfigurators.Applications
{
    class ApplicationEndpointEntityConfiguration : EntityTypeConfigurationBase<ApplicationEndpointEntity>
    {
        public override void ConfigureEntity(EntityTypeBuilder<ApplicationEndpointEntity> builder)
        {
            builder.ToTable("ApplicationEndpoint");

            //builder.HasOne(e => e.Environment).WithMany().HasForeignKey(e => e.EnvironmentId);

            //builder.HasOne(e => e.Application).WithMany(e => e.Endpoints).HasForeignKey(e => e.ApplicationId);

            //builder.Property(e => e.EnvironmentId).IsEnvironmentId();

            //builder.Property(e => e.Path).HasMaxLength(400).IsRequired();

            //builder.Property(e => e.Comment).IsDescription();
        }
    }
}
