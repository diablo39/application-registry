using ApplicationRegistry.Database.EntityConfigurators;
using ApplicationRegistry.Domain.Entities.Redis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Infrastructure.Domain.EntityConfigurators.Redis
{
    class RedisEndpointEntityConfiguration : EntityTypeConfigurationBase<RedisEndpointEntity>
    {
        public override void ConfigureEntity(EntityTypeBuilder<RedisEndpointEntity> builder)
        {
            builder.ToTable("RedisEndpoints");
        }
    }

    class BulkLoadRedisEndpointEntityConfiguration : EntityTypeConfigurationBase<BulkLoadRedisEndpointEntity>
    {
        public override void ConfigureEntity(EntityTypeBuilder<BulkLoadRedisEndpointEntity> builder)
        {
            builder.ToTable("RedisEndpoints", "BulkLoad");
        }
    }
}
