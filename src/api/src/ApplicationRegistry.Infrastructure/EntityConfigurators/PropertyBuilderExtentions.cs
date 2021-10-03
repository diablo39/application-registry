using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationRegistry.Infrastructure.EntityConfigurators
{
    public static class PropertyBuilderExtentions
    {
        public static PropertyBuilder<string> RulesForName(this PropertyBuilder<string> property)
        {
            return property.HasMaxLength(400)
                .IsRequired();
        }

        public static PropertyBuilder<string> RulesForDescription(this PropertyBuilder<string> property)
        {
            return property.HasMaxLength(1200);
        }
    }
}
