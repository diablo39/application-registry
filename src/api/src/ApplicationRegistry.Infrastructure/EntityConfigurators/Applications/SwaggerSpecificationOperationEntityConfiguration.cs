using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Database.EntityConfigurators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationRegistry.Infrastructure.Domain.EntityConfigurators.Specyfications
{
    class SwaggerSpecificationOperationEntityConfiguration : EntityTypeConfigurationBase<SwaggerSpecificationOperationEntity>
    {
        public override void ConfigureEntity(EntityTypeBuilder<SwaggerSpecificationOperationEntity> builder)
        {
            builder.ToTable("SwaggerSpecificationOperation");

            builder.HasOne(e => e.Specification)
                .WithMany()
                .HasForeignKey(e => e.IdApplicationVersionSpecification);
        }
    }
}
