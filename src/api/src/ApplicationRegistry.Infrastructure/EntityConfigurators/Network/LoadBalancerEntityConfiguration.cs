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

            builder.Property(e => e.Name)
                .IsName();

            builder.Property(e => e.Ip)
                .HasMaxLength(40);

            builder.Property(e => e.Port)
                .HasMaxLength(40);

            builder.Property(e => e.Description)
                .IsDescription();

            builder.Property(e => e.Fqdn)
                .HasMaxLength(400);
        }
    }
}
