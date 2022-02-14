using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Database.EntityConfigurators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationRegistry.Infrastructure.Domain.EntityConfigurators.Specyfications
{
    class SwaggerApplicationVersionSpecificationEntityConfiguration : EntityTypeConfigurationBase<SwaggerApplicationVersionSpecificationEntity>
    {
        public override void ConfigureEntity(EntityTypeBuilder<SwaggerApplicationVersionSpecificationEntity> builder)
        {
            builder.ToTable("ApplicationVersionSpecification");

            builder
                .HasOne(e => e.ApplicationVersion)
                .WithMany(e => e.Specifications)
                .HasForeignKey(e => e.IdApplicationVersion);

            //builder.HasDiscriminator<string>("SpecificationType")
            //    .HasValue<SwaggerApplicationVersionSpecificationEntity>(SpecificationTypeEntity.Swagger);

            //builder
            //    .HasOne<SpecificationTypeEntity>()
            //    .WithMany()
            //    .HasForeignKey(e => e.SpecificationType);
        }
    }
}
