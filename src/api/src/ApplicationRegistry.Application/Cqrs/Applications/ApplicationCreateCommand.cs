using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ApplicationRegistry.CQRS.Abstraction;
using System;
using System.Linq;
using System.Threading.Tasks;
using ApplicationRegistry.Domain.Entities.Applications;
using ApplicationRegistry.Application.Cqrs;
using System.Collections.Generic;
using ApplicationRegistry.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace ApplicationRegistry.Application.Commands
{
    public class ApplicationCreateCommand : ApplicationCommandBase, ICommand
    {
        public List<ApplicationCommandBaseEndpoint> Endpoints { get; }

        public ApplicationCreateCommand()
        {
            Endpoints = new List<ApplicationCommandBaseEndpoint>();
        }
    }

    public class ApplicationCreateCommandValidator : ApplicationCommandValidatorBase<ApplicationCreateCommand>
    {
        public ApplicationCreateCommandValidator()
            :base()
        {

        }
    }

    public class ApplicationCreateCommandResult
    {
        public Guid Id { get; set; }
    }

    public class ApplicationCreateCommandHandler : ICommandHandler<ApplicationCreateCommand, ApplicationCreateCommandResult>
    {
        private readonly IUnitOfWork _context;
        private readonly IGuidGenerator _guidGenerator;

        public ApplicationCreateCommandHandler(IUnitOfWork context, IGuidGenerator guidGenerator)
        {
            _context = context;
            _guidGenerator = guidGenerator;
        }

        public async Task<OperationResult<ApplicationCreateCommandResult>> ExecuteAsync(ApplicationCreateCommand command)
        {
            var application = new ApplicationEntity(_guidGenerator.CreateNewSequentialGuid(), command.Name, command.Code, command.IdProject)
            {
                BuildProcessUrls = command.BuildProcessUrls,
                Description = command.Description,
                Owner = command.Owner,
                RepositoryUrl = command.RepositoryUrl,
                Framework = command.Framework,
            };

            foreach (var endpoint in command.Endpoints)
            {
                var applicationEndpoint = new ApplicationEndpointEntity(_guidGenerator.CreateNewSequentialGuid(), endpoint.EnvironmentId, application.Id, endpoint.Path)
                {
                    Comment = endpoint.Comment,
                };

                application.Endpoints.Add(applicationEndpoint);
            }

            _context.ApplicationsRepository.Add(application);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return OperationResult.BusinessError<ApplicationCreateCommandResult>(new Dictionary<string, string> { { string.Empty, ex.Message } });
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
