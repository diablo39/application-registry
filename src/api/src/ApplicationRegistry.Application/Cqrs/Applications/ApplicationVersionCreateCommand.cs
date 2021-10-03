using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Commands
{
    public class ApplicationVersionCreateCommand : ICommand
    {
        public Guid ApplicationId { get; set; }

        public string EnvironmentId { get; set; }

        public string FrameworkVersion { get; set; }

        public string Version { get; set; }
    }

    public class ApplicationVersionCreateCommandValidator : AbstractValidator<ApplicationVersionCreateCommand>
    {
        public ApplicationVersionCreateCommandValidator()
        {

        }
    }

    public class ApplicationVersionCreateCommandResult
    {
        public Guid Id { get; set; }
    }

    public class ApplicationVersionCreateCommandHandler : ICommandHandler<ApplicationVersionCreateCommand, ApplicationVersionCreateCommandResult>
    {
        private readonly IUnitOfWork _context;

        public ApplicationVersionCreateCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<OperationResult<ApplicationVersionCreateCommandResult>> ExecuteAsync(ApplicationVersionCreateCommand command)
        {
            var applicationVersion = new ApplicationVersionEntity
            {
                IdApplication = command.ApplicationId,
                IdEnvironment = command.EnvironmentId,
                Version = command.Version,
                FrameworkVersion = command.FrameworkVersion,
            };
            _context.ApplicationVersions.Add(applicationVersion);

            await _context.SaveChangesAsync();
            var result = new ApplicationVersionCreateCommandResult { Id = applicationVersion.Id };

            return OperationResult.Success(result);
        }
    }

    public static class ApplicationVersionCreateCommandRegistration
    {
        public static void RegisterApplicationVersionCreateCommand(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<ApplicationVersionCreateCommand, ApplicationVersionCreateCommandResult>, ApplicationVersionCreateCommandHandler>();
            services.AddTransient<IValidator<ApplicationVersionCreateCommand>, ApplicationVersionCreateCommandValidator>();
        }
    }
}
