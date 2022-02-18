using ApplicationRegistry.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApplicationRegistry.Database.EntityConfigurators;

namespace ApplicationRegistry.Infrastructure.Domain.EntityConfigurators.Dependencies
{
    class DependencyEntityConfiguration : EntityTypeConfigurationBase<DependencyEntity>
    {
        public override void ConfigureEntity(EntityTypeBuilder<DependencyEntity> builder)
        {
            builder.ToTable("Dependency");

            builder
                .Property(e => e.Id)
                .HasConversion(s => s, s => s.ToLower());

            builder
                .HasOne(e => e.DependencyType)
                .WithMany(e => e.Dependencies)
                .HasForeignKey(e => e.IdDependencyType);

            builder
                .HasDiscriminator<string>("IdDependencyType")
                    .HasValue<DependencyEntity>(DependencyTypes.Nuget);
        }
    }
}
