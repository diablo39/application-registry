using ApplicationRegistry.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ApplicationRegistry.Database.EntityConfigurators
{
    class ApplicationEntityConfiguration : EntityTypeConfigurationBase<ApplicationEntity>
    {
        public override void ConfigureEntity(EntityTypeBuilder<ApplicationEntity> builder)
        {
            builder.ToTable("Application");

            builder.HasOne(e => e.System)
                    .WithMany(e => e.Applications)
                    .HasForeignKey(e => e.IdSystem);

            builder.HasMany(e => e.Endpoints)
                .WithOne(e => e.Application)
                .HasForeignKey(e => e.ApplicationId);

            builder.HasIndex(u => u.Code).IsUnique();

            var versionsNavigation = builder.Metadata.FindNavigation(nameof(ApplicationEntity.Versions));
            versionsNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
            versionsNavigation.SetField("_versions");
        }
    }
}
