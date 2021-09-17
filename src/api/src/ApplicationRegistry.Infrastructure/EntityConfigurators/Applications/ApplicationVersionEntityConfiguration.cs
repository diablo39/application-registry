using ApplicationRegistry.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationRegistry.Database.EntityConfigurators
{
    class ApplicationVersionEntityConfiguration : EntityTypeConfigurationBase<ApplicationVersionEntity>
    {
        public override void ConfigureEntity(EntityTypeBuilder<ApplicationVersionEntity> builder)
        {
            builder.ToTable("ApplicationVersion");

            builder
              .Property(e => e.ValidationStatus)
              .HasDefaultValueSql("(0)");

            builder
                .HasOne(a => a.Application)
                .WithMany(e => e.Versions)
                .HasForeignKey(e => e.IdApplication);

            builder
                .HasOne(a => a.Environment)
                .WithMany()
                .HasForeignKey(e => e.IdEnvironment);
        }
    }
}
