using ApplicationRegistry.Database.EntityConfigurators;
using ApplicationRegistry.Domain.Entities.Network;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Infrastructure.EntityConfigurators.Network
{
    class LoadBalancerEntityConfiguration : EntityTypeConfigurationBase<LoadBalancerEntity>
    {
        public override void ConfigureEntity(EntityTypeBuilder<LoadBalancerEntity> builder)
        {
            builder.ToTable("LoadBalancer");

            //builder.HasMany(e => e.Endpoints)
            //    .WithOne(e => e.Application)
            //    .HasForeignKey(e => e.ApplicationId);

        }
    }
}
