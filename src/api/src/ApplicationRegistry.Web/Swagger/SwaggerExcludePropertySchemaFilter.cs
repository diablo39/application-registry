using ApplicationRegistry.Application.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ApplicationRegistry.Web.Swagger
{
    public class SwaggerExcludePropertySchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null)
            {
                return;
            }

            var excludedProperties = context.Type.GetCustomAttributes<SwaggerIgnorePropertyAttribute>()?.ToList();

            if (excludedProperties != null && excludedProperties.Count > 0)

                foreach (var excludedProperty in excludedProperties)
                {
                    var propertyToRemove = schema.Properties.Keys.SingleOrDefault(x => string.Equals(x, excludedProperty.Name, StringComparison.OrdinalIgnoreCase));

                    if (propertyToRemove != null)
                    {
                        schema.Properties.Remove(propertyToRemove);
                    }
                }
        }
    }
}
