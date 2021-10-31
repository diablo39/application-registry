using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Database.EntityConfigurators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationRegistry.Infrastructure.Domain.EntityConfigurators.Specyfications
{
    class ApplicationVersionSpecificationEntityConfiguration : EntityTypeConfigurationBase<ApplicationVersionSpecificationEntity>
    {
        public override void ConfigureEntity(EntityTypeBuilder<ApplicationVersionSpecificationEntity> builder)
        {
            builder.ToTable("ApplicationVersionSpecification");

            builder
                .HasOne(e => e.ApplicationVersion)
                .WithMany(e => e.Specifications)
                .HasForeignKey(e => e.IdApplicationVersion);

            builder.HasDiscriminator<string>("SpecificationType")
                .HasValue<SwaggerApplicationVersionSpecificationEntity>(SpecificationTypeEntity.Swagger);

            builder
                .HasOne<SpecificationTypeEntity>()
                .WithMany()
                .HasForeignKey(e => e.SpecificationType);
        }
    }
}
