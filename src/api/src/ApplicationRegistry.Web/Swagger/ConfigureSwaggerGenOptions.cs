using ApplicationRegistry.Web.Areas.Api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace ApplicationRegistry.Web.Swagger
{
    public class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IConfiguration _configuration;

        public ConfigureSwaggerGenOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(SwaggerGenOptions options)
        {
            var apiinfo = new OpenApiInfo
            {
                Title = "Application Registry API",
                Version = "v1",
                Description = "",
            };

            options.SwaggerDoc("v1", apiinfo);

            var oidcFlow = new OpenApiSecurityScheme
            {
                Description = "Get a bearer token using OAUTH & OIDC.",
                Name = "Authorization",
                Type = SecuritySchemeType.OpenIdConnect,
                OpenIdConnectUrl = new Uri($"{_configuration["Authentication:Authority"]}/.well-known/openid-configuration"),
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {

                    },
                },
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "OIDC Flow"
                },
            };

            options.AddSecurityDefinition("OIDC Flow", oidcFlow);

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    oidcFlow,
                    new List<string>()
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
            options.DescribeAllParametersInCamelCase();
            options.RequestBodyFilter<SwaggerExcludePropertyBodyFilter>();
            options.OperationFilter<AddDefaultResponse<ApiErrorModel>>();
        }
    }

    public class AddDefaultResponse<T> : IOperationFilter
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
