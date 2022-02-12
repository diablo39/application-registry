using ApplicationRegistry.Application;
using ApplicationRegistry.Application.Services;
using ApplicationRegistry.Infrastructure;
using ApplicationRegistry.Infrastructure.HangfireExtensions;
using ApplicationRegistry.Infrastructure.UnitOfWork;
using ApplicationRegistry.Web.Areas.Api.Models;
using ApplicationRegistry.Web.Models;
using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ApplicationRegistry.Web
{
    public partial class Startup
    {
        private IConfiguration Configuration { get; }

        private IWebHostEnvironment Env { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCompression();

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            services.AddSwagger();

            services.AddMvc(options =>
                {
                    options.Filters.Add(new ConsumesAttribute("application/json"));
                    options.Filters.Add(new ProducesAttribute("application/json"));
                })
               .AddFluentValidation(conf => { })
               .AddNewtonsoftJson(e => { })
               .ConfigureApiBehaviorOptions(options =>
               {
                   options.InvalidModelStateResponseFactory = context =>
                   {
                       var problems = new ApiErrorModel(context);
                       return new UnprocessableEntityObjectResult(problems);
                   };
               });

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddLocalization();

            services.Configure<SotDataProviderConfiguration>(Configuration.GetSection("SotDataProvider"));

            services.Configure<ApplicationConfiguration>(Configuration);

            services.AddDbContext<ApplicationRegistryDatabaseContext>(options =>
                options
                    .UseSqlServer(Configuration.GetConnectionString("ApplicationRegistry"), b => b.MigrationsAssembly("ApplicationRegistry.Web").UseHierarchyId())
                .EnableSensitiveDataLogging()
                //.ConfigureWarnings(e => e.Ignore(CoreEventId.IncompatibleMatchingForeignKeyProperties))
                , ServiceLifetime.Scoped);


            services.AddHangfire(x => x
                .UseSqlServerStorage(Configuration.GetConnectionString("ApplicationRegistry"))
                .UseSerilogLogProvider());

            services.RegisterApplicationServices();
            services.RegisterInfrastructureServices();

            services.RegisterCommandsAndQueries();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = Configuration["Authentication:Authority"];
                    options.RequireHttpsMetadata = false;
                    options.Audience = Configuration["Authentication:Audience"];
                    options.TokenValidationParameters.ValidateIssuer = false;
                });

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            services.AddHangfireServer(backgroundServer => backgroundServer.WorkerCount = 5);

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IServiceProvider provider)
        {
            provider.InitializeServices();

            app.UseResponseCompression();

            app.UseDeveloperExceptionPage();

            var supportedCultures = new[] { "en-US", "pl-PL", "pl", "en" };

            app.UseRequestLocalization(options => options
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures));

            app.UseRouting();

            app.UseSwagger(c =>
            {
                //c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
                c.DisplayOperationId();
                c.DisplayRequestDuration();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ApplicationServices.CreateScope().ServiceProvider.GetService<ApplicationRegistryDatabaseContext>().Database.Migrate();

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyAuthorizationFilter() },

                //IsReadOnlyFunc = (DashboardContext context) => true
            });
        }
    }
}
