using ApplicationRegistry.Application.AsyncJobs;
using ApplicationRegistry.Application.CommandHandlers;
using ApplicationRegistry.Application.Commands;
using ApplicationRegistry.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ApplicationRegistry.Application
{
    public static class Registrations
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ProcessSpecifiacationTextChangedJob>();

            services.AddTransient<IValidator<CollectCliResultCommand>, CollectCliResultCommandValidator>();
            services.AddScoped<CollectCliResultCommandHandler>();

            services.AddSingleton<SotDataProvider>();
        }
    }
}
