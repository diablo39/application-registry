using ApplicationRegistry.Database.EntityConfigurators;
using ApplicationRegistry.Domain.Entities.Redis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Infrastructure.Domain.EntityConfigurators.Redis
{
    class RedisDeploymentTypeEntityConfiguration : EntityTypeConfigurationBase<RedisDeploymentTypeEntity>
    {
        public override void ConfigureEntity(EntityTypeBuilder<RedisDeploymentTypeEntity> builder)
        {
            builder.ToTable("RedisDeploymentTypes");

            builder.HasData(
                new RedisDeploymentTypeEntity { Id = "SENTINEL", Description = "Sentinel", CreateDate = new DateTime(1900,01,01) },
                new RedisDeploymentTypeEntity { Id = "MASTER_SLAVE", Description = "Master-Slave", CreateDate = new DateTime(1900, 01, 01) },
                new RedisDeploymentTypeEntity { Id = "CLUSTER", Description = "Cluster", CreateDate = new DateTime(1900, 01, 01) }
                );
        }
    }
}
