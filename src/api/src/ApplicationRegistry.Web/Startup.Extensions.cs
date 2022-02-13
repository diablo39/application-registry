using ApplicationRegistry.Application.CommandDecorators;
using ApplicationRegistry.Application.QueryDecorators;
using ApplicationRegistry.Application.Services;
using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Web.Swagger;
using FluentValidation;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.IO.Compression;
using System.Reflection;

namespace ApplicationRegistry.Web
{
    internal static class StartupExtentions
    {
        public static void AddCompression(this IServiceCollection services)
        {
            services.AddResponseCompression(e => { e.EnableForHttps = true; });
            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });
        }

        public static void InitializeServices(this IServiceProvider provider)
        {
            provider.GetRequiredService<SotDataProvider>();
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Application Registry API",
                    Description = "A simple example ASP.NET Core Web API",
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.DescribeAllParametersInCamelCase();
                c.RequestBodyFilter<SwaggerExcludePropertyBodyFilter>();
                
            });

            services.AddFluentValidationRulesToSwagger();
        }

        public static void RegisterCommandsAndQueries(this IServiceCollection services)
        {
            services.Scan(scan => scan
                            .FromAssemblies(typeof(ApplicationRegistry.Application.Registrations).Assembly)
                                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>))).AsImplementedInterfaces().WithScopedLifetime()
                                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>))).AsImplementedInterfaces().WithScopedLifetime()
                                .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>))).AsImplementedInterfaces().WithTransientLifetime()
                            );

            services.Decorate(typeof(ICommandHandler<,>), typeof(OperationTypeCommandHandlerDecorator<,>));
            services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationCommandHandlerDecorator<,>));
            services.Decorate(typeof(ICommandHandler<,>), typeof(CatchExceptionsCommandHandlerDecorator<,>));

            services.Decorate(typeof(IQueryHandler<,>), typeof(OperationTypeQueryHandlerDecorator<,>));
            services.Decorate(typeof(IQueryHandler<,>), typeof(CatchExceptionsQueryHandlerDecorator<,>));
        }
    }
}
