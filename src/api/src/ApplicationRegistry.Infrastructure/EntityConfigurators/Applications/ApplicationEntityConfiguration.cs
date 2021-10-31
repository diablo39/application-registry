using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Infrastructure.EntityConfigurators;
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

            builder.Property(e => e.Name)
                .IsName();

            builder.Property(e => e.Code)
                .HasMaxLength(160)
                .IsRequired();

            builder.Property(e => e.IdSystem)
                .IsRequired()
                .HasColumnName("IdSystem");

            builder.Property(e => e.Description)
                .IsDescription();

            builder.Property(e => e.Owner)
                .HasMaxLength(250);

            builder.Property(e => e.RepositoryUrl)
                .HasMaxLength(400);

            builder.Property(e => e.BuildProcessUrls)
                .HasMaxLength(400);

            builder.Property(e => e.Framework)
                .HasMaxLength(250);

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
