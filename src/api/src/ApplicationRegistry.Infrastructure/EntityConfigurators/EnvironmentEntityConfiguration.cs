using ApplicationRegistry.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationRegistry.Database.EntityConfigurators
{
    class EnvironmentEntityConfiguration : EntityTypeConfigurationBase<EnvironmentEntity>
    {
        public override void ConfigureEntity(EntityTypeBuilder<EnvironmentEntity> builder)
        {
            builder.ToTable("Environment");
        }
    }
}
