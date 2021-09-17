using ApplicationRegistry.Application;
using ApplicationRegistry.Application.CommandDecorators;
using ApplicationRegistry.Application.QueryDecorators;
using ApplicationRegistry.Application.Services;
using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Infrastructure;
using ApplicationRegistry.Infrastructure.HangfireExtensions;
using ApplicationRegistry.Web.Areas.Api.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
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
               .AddNewtonsoftJson()
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

            //services
            //    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.Authority = "https://dev-hdra111x.eu.auth0.com";
            //    });
                //.AddCookie(setup => {  })
                //.AddOpenIdConnect("OIDC", options =>
                //{
                //    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                //    // Set the authority to your Auth0 domain
                //    options.Authority = $"https://{Configuration["Auth0:Domain"]}";

                //    // Configure the Auth0 Client ID and Client Secret
                //    options.ClientId = Configuration["Auth0:ClientId"];
                //    options.ClientSecret = Configuration["Auth0:ClientSecret"];

                //    // Set response type to code
                //    options.ResponseType = "code";

                //    // Configure the scope
                //    options.Scope.Clear();
                //    options.Scope.Add("openid");

                //    // Set the callback path, so Auth0 will call back to http://localhost:3000/callback
                //    // Also ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard
                //    options.CallbackPath = new PathString("/api/signin-oidc");

                //    // Configure the Claims Issuer to be Auth0
                //    options.ClaimsIssuer = "Auth0";
                //    options.CorrelationCookie.SameSite = SameSiteMode.None;
                //    options.NonceCookie.SameSite = SameSiteMode.None;
                //});
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

            //app.UseAuthentication();

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

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ApplicationServices.CreateScope().ServiceProvider.GetService<ApplicationRegistryDatabaseContext>().Database.Migrate();

            app.UseHangfireServer(new BackgroundJobServerOptions { WorkerCount = 5 });

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyAuthorizationFilter() },

                //IsReadOnlyFunc = (DashboardContext context) => true
            });
        }
    }
}
