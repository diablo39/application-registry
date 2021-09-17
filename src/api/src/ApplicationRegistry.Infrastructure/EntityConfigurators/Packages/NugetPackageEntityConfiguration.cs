using ApplicationRegistry.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationRegistry.Infrastructure.Domain.EntityConfigurators.Dependencies
{
    public class NugetPackageEntityConfiguration : IEntityTypeConfiguration<NugetPackageEntity>
    {
        public void Configure(EntityTypeBuilder<NugetPackageEntity> builder)
        {
            builder.ToTable("NugetPackage");

            builder.HasKey(e => new { e.Id });

            builder.Property(p => p.Id).ValueGeneratedOnAdd().UseIdentityColumn();

            builder.Property(e => e.Name).IsRequired().HasMaxLength(400);

            builder.Property(e => e.Version).IsRequired().HasMaxLength(400);

            builder
                .Property(e => e.CreateDate)
                .IsRequired(true)
                .HasDefaultValueSql("getdate()")
                .ValueGeneratedOnAddOrUpdate();

            builder.HasIndex(u => new { u.Name, u.Version}).IsUnique();

            builder.HasIndex(u => new { u.Name }).IsUnique(false);
        }
    }
}
//