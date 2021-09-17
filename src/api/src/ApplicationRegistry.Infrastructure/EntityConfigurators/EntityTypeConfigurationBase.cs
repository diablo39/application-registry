using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationRegistry.Database.EntityConfigurators
{
    abstract class EntityTypeConfigurationBase<T> : IEntityTypeConfiguration<T>
        where T: class, IEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            EnsureIdExists();

            builder.HasKey("Id");

            builder
                .Property(e => e.CreateDate)
                .ValueGeneratedOnAddOrUpdate()
                .IsRequired(true)
                .HasDefaultValueSql("getdate()");

            ConfigureEntity(builder);
        }

        private void EnsureIdExists()
        {
            var entityType = typeof(T);
            var idProperty = entityType.GetProperty("Id");
            if (idProperty == null)
                throw new InvalidCastException(string.Format("Type: {0} is not implementing IEntity<T> interface", entityType.ToString()));
        }

        public abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
    }
}
