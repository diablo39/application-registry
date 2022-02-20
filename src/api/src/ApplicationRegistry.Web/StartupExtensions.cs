using ApplicationRegistry.Application.CommandDecorators;
using ApplicationRegistry.Application.QueryDecorators;
using ApplicationRegistry.Application.Services;
using ApplicationRegistry.CQRS.Abstraction;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Compression;

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
