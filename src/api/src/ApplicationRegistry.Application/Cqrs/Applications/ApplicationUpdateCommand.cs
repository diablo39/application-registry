using ApplicationRegistry.Application.Services;
using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Domain.Entities.Applications;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Commands
{
    public class ApplicationUpdateCommand : ApplicationCommandBase, ICommand
    {
        public Guid Id { get; set; }

        public List<ApplicationUpdateCommandEndpoint> Endpoints { get; }

        public ApplicationUpdateCommand()
        {
            Endpoints = new List<ApplicationUpdateCommandEndpoint>();
        }
    }

    public class ApplicationUpdateCommandEndpoint : ApplicationCommandBaseEndpoint
    {
        public Guid? Id { get; set; }
    }

    public class ApplicationUpdateCommandValidator : ApplicationCommandValidatorBase<ApplicationUpdateCommand>
    {
        public ApplicationUpdateCommandValidator()
            : base()
        {
            //RuleForEach(e => e.Endpoints).SetValidator(new ApplicationCommandEndpointValidator());
        }
    }

    public class ApplicationCommandEndpointValidator : AbstractValidator<ApplicationCommandBaseEndpoint>
    {
        public ApplicationCommandEndpointValidator()
        {
            RuleFor(e => e.EnvironmentId).NotEmpty().NotNull();
            RuleFor(e => e.Path).NotEmpty().NotNull();
        }
    }

    public class ApplicationUpdateCommandResult
    {
        public Guid Id { get; set; }
    }

    public class ApplicationUpdateCommandHandler : ICommandHandler<ApplicationUpdateCommand, ApplicationUpdateCommandResult>
    {
        private readonly IUnitOfWork _context;
        private readonly IGuidGenerator _guidGenerator;

        public ApplicationUpdateCommandHandler(IUnitOfWork context, IGuidGenerator guidGenerator)
        {
            _context = context;
            _guidGenerator = guidGenerator;
        }

        public async Task<OperationResult<ApplicationUpdateCommandResult>> ExecuteAsync(ApplicationUpdateCommand command)
        {
            var application = await _context.ApplicationsRepository.GetWithEndpointsAsync(command.Id); 

            if (application == null)
            {
                return OperationResult.BusinessError<ApplicationUpdateCommandResult>(new KeyValuePair<string, string>(nameof(command.Id), $"Application with id: { command.Id } not exists"));
            }

            application.BuildProcessUrls = command.BuildProcessUrls;
            application.Code = command.Code;
            application.Description = command.Description;
            application.IdSystem = command.IdProject;
            application.Name = command.Name;
            application.Owner = command.Owner;
            application.RepositoryUrl = command.RepositoryUrl;
            application.Framework = command.Framework;

            var endpoints = command.Endpoints.Select(e => new ApplicationEndpointEntity(e.Id ?? _guidGenerator.CreateNewSequentialGuid(), e.EnvironmentId, application.Id, e.Path)
            {
                Comment = e.Comment,
            });

            _context.ApplicationsRepository.UpdateChildCollection<ApplicationEndpointEntity, Guid>(
                application,
                e => e.Endpoints,
                endpoints,
                (existingItem, updatedItem) =>
                {
                    existingItem.Comment = updatedItem.Comment;
                    existingItem.EnvironmentId = updatedItem.EnvironmentId;
                    existingItem.Path = updatedItem.Path;
                }
            );

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return OperationResult.BusinessError<ApplicationUpdateCommandResult>(new Dictionary<string, string> { { string.Empty, ex.Message } });
            }
            catch (Exception)
            {
                throw;
            }

            var result = new ApplicationUpdateCommandResult { Id = application.Id };

            return OperationResult.Success(result);
        }
    }
}
