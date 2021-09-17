using ApplicationRegistry.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Database.EntityConfigurators
{
    class CollectorKnowledgeBaseEntityConfiguration : EntityTypeConfigurationBase<CollectorKnowledgeBaseEntity>
    {
        public override void ConfigureEntity(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CollectorKnowledgeBaseEntity> builder)
        {
            builder.ToTable("CollectorKnowledgeBase");
        }
    }
}
