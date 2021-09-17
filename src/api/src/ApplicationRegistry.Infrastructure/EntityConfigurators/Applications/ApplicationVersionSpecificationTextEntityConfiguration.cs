using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Database.EntityConfigurators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Infrastructure.Domain.EntityConfigurators.Specyfications
{
    class ApplicationVersionSpecificationTextEntityConfiguration : EntityTypeConfigurationBase<ApplicationVersionSpecificationTextEntity>
    {
        public override void ConfigureEntity(EntityTypeBuilder<ApplicationVersionSpecificationTextEntity> builder)
        {
            builder.ToTable("ApplicationVersionSpecificationText");
        }
    }
}
