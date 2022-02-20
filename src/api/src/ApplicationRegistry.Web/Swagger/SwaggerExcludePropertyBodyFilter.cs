using ApplicationRegistry.Application.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ApplicationRegistry.Web.Swagger
{
    public class SwaggerExcludePropertyBodyFilter : IRequestBodyFilter
    {
        public void Apply(OpenApiRequestBody requestBody, RequestBodyFilterContext context)
        {
            var excludedProperties = context.BodyParameterDescription.CustomAttributes().OfType<SwaggerIgnorePropertyAttribute>()?.ToList();

            foreach (var excludedProperty in excludedProperties)
            {
                var typeName = context.BodyParameterDescription.Type.Name;
                var FullTypeName = context.BodyParameterDescription.Type.FullName;

                var schemaKey = context.SchemaRepository.Schemas.Keys.FirstOrDefault(x => string.Equals(x, typeName, StringComparison.OrdinalIgnoreCase) || string.Equals(x, FullTypeName, StringComparison.OrdinalIgnoreCase));

                if (string.IsNullOrWhiteSpace(schemaKey))
                    continue;

                var schema = context.SchemaRepository.Schemas[schemaKey];

                foreach (var excludedPropertyName in excludedProperty.Names)
                {
                    var propertyToRemove = schema.Properties.Keys.SingleOrDefault(x => string.Equals(x, excludedPropertyName, StringComparison.OrdinalIgnoreCase));

                    if (propertyToRemove != null)
                    {
                        var removalResult = schema.Properties.Remove(propertyToRemove);
                    }
                }
            }
        }
    }
}
