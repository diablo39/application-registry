using ApplicationRegistry.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApplicationRegistry.Database.EntityConfigurators;
using ApplicationRegistry.Database;

namespace ApplicationRegistry.Infrastructure.Domain.EntityConfigurators.Specyfications
{
    class SpecificationTypeEntityConfiguration : EntityTypeConfigurationBase<SpecificationTypeEntity>
    {
        public override void ConfigureEntity(EntityTypeBuilder<SpecificationTypeEntity> builder)
        {
            builder.ToTable("SpecificationTypes");

            builder.HasData(
                new SpecificationTypeEntity { Id = "Swagger", Name = "Swagger", CreateDate = Consts.CreateDate }
            );
        }
    }
}
