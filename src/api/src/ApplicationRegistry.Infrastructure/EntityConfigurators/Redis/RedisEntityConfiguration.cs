using ApplicationRegistry.Database.EntityConfigurators;
using ApplicationRegistry.Domain.Entities.Redis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Infrastructure.Domain.EntityConfigurators.Redis
{
    class RedisEntityConfiguration : EntityTypeConfigurationBase<RedisEntity>
    {
        public override void ConfigureEntity(EntityTypeBuilder<RedisEntity> builder)
        {
            builder.ToTable("Redis");

            builder.HasOne(e => e.RedisDeploymentType)
                    .WithMany()
                    .HasForeignKey(e => e.RedisDeploymentTypeId);

            builder.HasMany(e => e.Endpoints)
                .WithOne()
                .HasForeignKey(e => e.RedisId);

            builder
                .HasOne(a => a.Environment)
                .WithMany()
                .HasForeignKey(e => e.IdEnvironment);
        }
    }

    class BulkLoadRedisEntityConfiguration : EntityTypeConfigurationBase<BulkLoadRedisEntity>
    {
        public override void ConfigureEntity(EntityTypeBuilder<BulkLoadRedisEntity> builder)
        {
            builder.ToTable("Redis", "BulkLoad");

            builder.HasOne(e => e.RedisDeploymentType)
                    .WithMany()
                    .HasForeignKey(e => e.RedisDeploymentTypeId);

            builder.HasMany(e => e.Endpoints)
                .WithOne()
                .HasForeignKey(e => e.RedisId);

            builder
                .HasOne(a => a.Environment)
                .WithMany()
                .HasForeignKey(e => e.IdEnvironment);
        }
    }
}
