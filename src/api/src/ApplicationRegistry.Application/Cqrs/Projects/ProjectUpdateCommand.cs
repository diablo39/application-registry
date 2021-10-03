using ApplicationRegistry.Application.Attributes;
using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Commands
{
    
    public class ProjectUpdateCommand : ICommand
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class ProjectUpdateCommandValidator : AbstractValidator<ProjectUpdateCommand>
    {
        public ProjectUpdateCommandValidator()
        {
            RuleFor(e => e.Id).NotNull();
            RuleFor(e => e.Name).NotEmpty();
        }
    }

    public class ProjectUpdateCommandResult
    {
        public Guid Id { get; set; }
    }

    public class ProjectUpdateCommandHandler : ICommandHandler<ProjectUpdateCommand, ProjectUpdateCommandResult>
    {
        private readonly IUnitOfWork _context;

        public ProjectUpdateCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<OperationResult<ProjectUpdateCommandResult>> ExecuteAsync(ProjectUpdateCommand command)
        {
            var project = _context.ProjectsRepository.Get(command.Id);

            if (project == null)
            {
                project = new ProjectEntity(command.Id ?? Guid.NewGuid(), command.Name);
                _context.ProjectsRepository.Add(project);
            }

            project.Name = command.Name;
            project.Description = command.Description;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return OperationResult.BusinessError<ProjectUpdateCommandResult>(new Dictionary<string, string> { { "Id", $"Project with id: {command.Id} already exists" } });
            }
            catch (Exception)
            {
                throw;
            }

            var result = new ProjectUpdateCommandResult { Id = project.Id };

            return OperationResult.Success(result);
        }
    }
}
