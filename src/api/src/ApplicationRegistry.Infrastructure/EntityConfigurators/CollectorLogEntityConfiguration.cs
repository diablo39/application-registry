using ApplicationRegistry.Database.EntityConfigurators;
using ApplicationRegistry.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Infrastructure.Domain.EntityConfigurators
{
    class CollectorLogEntityConfiguration : EntityTypeConfigurationBase<CollectorLogEntity>
    {
        public override void ConfigureEntity(EntityTypeBuilder<CollectorLogEntity> builder)
        {
            builder.Property(e => e.Id).UseIdentityColumn(seed: int.MinValue);
        }
    }

}
