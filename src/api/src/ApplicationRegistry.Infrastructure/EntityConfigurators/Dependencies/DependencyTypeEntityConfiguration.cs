using ApplicationRegistry.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using DependencyTypesEnum = ApplicationRegistry.Database.Entities.DependencyTypes;
using ApplicationRegistry.Database.EntityConfigurators;
using ApplicationRegistry.Database;

namespace ApplicationRegistry.Infrastructure.Domain.EntityConfigurators.Dependencies
{
    class DependencyTypeEntityConfiguration : EntityTypeConfigurationBase<DependencyTypeEntity>
    {
        public override void ConfigureEntity(EntityTypeBuilder<DependencyTypeEntity> builder)
        {
            builder.ToTable("DependencyType");

            builder.HasData(new DependencyTypeEntity
            {
                Id = DependencyTypesEnum.Application,
                CreateDate = Consts.CreateDate,
                Name = "Application",
                CanBeAddedManualy = true
            });
        }
    }
}
