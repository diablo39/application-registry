using ApplicationRegistry.Database.EntityConfigurators;
using ApplicationRegistry.Domain.Entities.Network;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationRegistry.Infrastructure.EntityConfigurators.Network
{
    class VlanEntityConfiguration : EntityTypeConfigurationBase<VlanEntity>
    {
        public override void ConfigureEntity(EntityTypeBuilder<VlanEntity> builder)
        {
            builder.ToTable("Vlan");

            builder.Property(e => e.Name)
                .HasMaxLength(400)
                .IsRequired();

            builder.Property(e => e.Number);

            builder.Property(e => e.Alias)
                .HasMaxLength(400);

            builder.Property(e => e.Cidr)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(e => e.Description)
                .HasMaxLength(1200);

            builder.Property(e => e.RFC)
                .HasMaxLength(400);
        }
    }
}
