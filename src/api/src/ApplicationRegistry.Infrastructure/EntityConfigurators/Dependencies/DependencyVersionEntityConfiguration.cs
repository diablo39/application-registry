using ApplicationRegistry.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApplicationRegistry.Database.EntityConfigurators;

namespace ApplicationRegistry.Infrastructure.Domain.EntityConfigurators.Dependencies
{
    class DependencyVersionEntityConfiguration : EntityTypeConfigurationBase<DependencyVersionEntity>
    {
        public override void ConfigureEntity(EntityTypeBuilder<DependencyVersionEntity> builder)
        {
            builder.ToTable("DependencyVersion");

            builder
                .Property(e => e.Id)
                .HasConversion(s => s, s => s.ToLower());

            builder.Property(e => e.IdDependency)
                .HasColumnName("DependencyId");

            builder
                .Property(e => e.ValidationStatus)
                .HasDefaultValueSql("(0)");

            builder
                .HasOne(e => e.Dependency)
                .WithMany(e => e.Versions)
                .HasForeignKey(e => e.IdDependency);
        }
    }
}
