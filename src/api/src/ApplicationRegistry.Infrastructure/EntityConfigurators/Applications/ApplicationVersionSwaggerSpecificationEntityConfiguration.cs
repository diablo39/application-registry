using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Database.EntityConfigurators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationRegistry.Infrastructure.Domain.EntityConfigurators.Specyfications
{
    class ApplicationVersionSwaggerSpecificationEntityConfiguration : EntityTypeConfigurationBase<ApplicationVersionSwaggerSpecificationEntity>
    {
        public override void ConfigureEntity(EntityTypeBuilder<ApplicationVersionSwaggerSpecificationEntity> builder)
        {
            builder.ToTable("ApplicationVersionSpecification");

            builder
                .HasOne(e => e.ApplicationVersion)
                .WithMany(e => e.SwaggerSpecifications)
                .HasForeignKey(e => e.IdApplicationVersion);
        }
    }
}
