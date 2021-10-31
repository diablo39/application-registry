
namespace Microsoft.EntityFrameworkCore.Metadata.Builders
{
    public static class PropertyBuilderExtentions
    {
        public static PropertyBuilder<string> IsName(this PropertyBuilder<string> property)
        {
            return property.HasMaxLength(400)
                .IsRequired();
        }

        public static PropertyBuilder<string> IsDescription(this PropertyBuilder<string> property)
        {
            return property.HasMaxLength(1200);
        }

        public static PropertyBuilder<string> IsEnvironmentId(this PropertyBuilder<string> property)
        {
            return property.HasMaxLength(25).IsRequired();
        }
    }
}
