using ApplicationRegistry.Database;
using ApplicationRegistry.Infrastructure.Abstractions.Scheduler;
using ApplicationRegistry.Infrastructure.Implementations.Scheduler;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Infrastructure
{
    public static class Registrations
    {
        public static void RegisterInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork>((s) => s.GetRequiredService<ApplicationRegistryDatabaseContext>());
            
            services.AddScoped<IQueryDataModel>(s =>
            {
                var result = s.GetRequiredService<ApplicationRegistryDatabaseContext>();
                result.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                return result;
            });

            services.AddScoped<IScheduler, Scheduler>();
        }
    }
}
