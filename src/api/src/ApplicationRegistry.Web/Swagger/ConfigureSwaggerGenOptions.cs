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
            options.OperationFilter<AddDefaultResponseFilter<ApiErrorModel>>();
        }
    }
}
