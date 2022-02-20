using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApplicationRegistry.Web.Swagger
{
    public class AddDefaultResponseFilter<T> : IOperationFilter
        where T : class
    {

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (!context.SchemaRepository.TryLookupByType(typeof(T), out var errorSchema))
            {
                errorSchema = context.SchemaGenerator.GenerateSchema(typeof(T), context.SchemaRepository);
            }

            AddResponse(operation, errorSchema, "400", "Bad request");
            AddResponse(operation, errorSchema, "401", "Unauthenticated");
            AddResponse(operation, errorSchema, "403", "Forbidden");
            AddResponse(operation, errorSchema, "422", "Validation error");
            AddResponse(operation, errorSchema, "500", "Server internal error");
        }

        private static void AddResponse(OpenApiOperation operation, OpenApiSchema errorSchema, string httpStatusCode, string description)
        {
            if (operation.Responses.ContainsKey(httpStatusCode)) return;

            operation.Responses.Add(httpStatusCode, new OpenApiResponse
            {
                Description = description,
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    {
                        "application/json",
                        new OpenApiMediaType
                        {
                            Schema = errorSchema
                        }
                    }
                }
            });
        }
    }
}
