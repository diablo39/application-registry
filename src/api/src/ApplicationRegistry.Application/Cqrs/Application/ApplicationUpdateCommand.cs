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

    }

    public class ApplicationUpdateCommandValidator : AbstractValidator<ApplicationUpdateCommand>
    {
        public ApplicationUpdateCommandValidator()
        {
            RuleFor(e => e.Name).NotEmpty();
            RuleFor(e => e.Code).NotEmpty();

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

        public ApplicationUpdateCommandHandler(IUnitOfWork context)
        {
            _context = context;
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
            application.IdProject = command.IdProject;
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
                    .Where(c => c.Id == childModel.Id && c.Id != default(Guid))
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
                        Id = Guid.NewGuid(),
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
