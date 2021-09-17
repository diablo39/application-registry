using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ApplicationRegistry.CQRS.Abstraction;
using System;
using System.Linq;
using System.Threading.Tasks;
using ApplicationRegistry.Domain.Entities.Applications;

namespace ApplicationRegistry.Application.Commands
{
    public class ApplicationCreateCommand : ApplicationCommandBase, ICommand
    {

    }

    public class ApplicationCreateCommandValidator : AbstractValidator<ApplicationCreateCommand>
    {
        public ApplicationCreateCommandValidator()
        {
            RuleFor(e => e.Name).NotEmpty();
            RuleFor(e => e.Code).NotEmpty();
        }
    }

    public class ApplicationCreateCommandResult
    {
        public Guid Id { get; set; }
    }

    public class ApplicationCreateCommandHandler : ICommandHandler<ApplicationCreateCommand, ApplicationCreateCommandResult>
    {
        private readonly IUnitOfWork _context;

        public ApplicationCreateCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<OperationResult<ApplicationCreateCommandResult>> ExecuteAsync(ApplicationCreateCommand command)
        {
            var application = new ApplicationEntity(Guid.NewGuid(), command.Name, command.Code, command.IdProject)
            {
                BuildProcessUrls = command.BuildProcessUrls,
                Description = command.Description,
                Owner = command.Owner,
                RepositoryUrl = command.RepositoryUrl,
                Framework = command.Framework,
            };

            _context.ApplicationsRepository.Add(application);

            foreach (var endpoint in command.Endpoints)
            {
                var applicationEndpoint = new ApplicationEndpointEntity
                {
                    ApplicationId = application.Id,
                    Comment = endpoint.Comment,
                    CreateDate = DateTime.Now,
                    EnvironmentId = endpoint.EnvironmentId,
                    Id = Guid.NewGuid(),
                    Path = endpoint.Path,
                };

                application.Endpoints.Add(applicationEndpoint);

                _context.ApplicationEndpoints.Add(applicationEndpoint);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            var result = new ApplicationCreateCommandResult { Id = application.Id };

            return OperationResult.Success(result);
        }
    }
}
