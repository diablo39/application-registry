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
        public List<ApplicationUpdateCommandEndpoint> Endpoints { get; }

        public ApplicationUpdateCommand()
        {
            Endpoints = new List<ApplicationUpdateCommandEndpoint>();
        }
    }

    public class ApplicationUpdateCommandEndpoint: ApplicationCommandBaseEndpoint
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
            var application = await _context.Applications.Include(e => e.Endpoints).SingleOrDefaultAsync(e => e.Id == command.Id);

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

            // Delete children
            foreach (var existingChild in application.Endpoints.Where(existingChild => !command.Endpoints.Any(c => c.Id == existingChild.Id)).ToList())
            {
                application.Endpoints.Remove(existingChild);
            }

            foreach (var childModel in command.Endpoints)
            {
                var existingChild = application.Endpoints
                    .Where(c => childModel.Id != null && c.Id == childModel.Id && c.Id != default)
                    .SingleOrDefault();

                if (existingChild != null)
                {
                    existingChild.Comment = childModel.Comment;
                    existingChild.EnvironmentId = childModel.EnvironmentId;
                    existingChild.Path = childModel.Path;
                }
                else
                {
                    var newChild = new ApplicationEndpointEntity
                    {
                        ApplicationId = application.Id,
                        Comment = childModel.Comment,
                        CreateDate = DateTime.Now,
                        EnvironmentId = childModel.EnvironmentId,
                        Id = _guidGenerator.CreateNewSequentialGuid(),
                        Path = childModel.Path
                    };
                    application.Endpoints.Add(newChild);

                    _context.ApplicationEndpoints.Add(newChild);
                }
            }

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
