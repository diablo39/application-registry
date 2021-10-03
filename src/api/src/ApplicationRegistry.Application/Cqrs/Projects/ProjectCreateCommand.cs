using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using FluentValidation;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Commands
{
    public class ProjectCreateCommand : ICommand
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class ProjectCreateCommandValidator : AbstractValidator<ProjectCreateCommand>
    {
        public ProjectCreateCommandValidator()
        {
            RuleFor(e => e.Name).NotEmpty();
        }
    }

    public class ProjectCreateCommandResult
    {
        public Guid Id { get; set; }
    }

    public class ProjectCreateCommandHandler : ICommandHandler<ProjectCreateCommand, ProjectCreateCommandResult>
    {
        private readonly IUnitOfWork _context;

        public ProjectCreateCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<OperationResult<ProjectCreateCommandResult>> ExecuteAsync(ProjectCreateCommand command)
        {
            var project = new ProjectEntity(command.Id == default ? Guid.NewGuid() : command.Id, command.Name) { Description = command.Description };

            _context.Projects.Add(project);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }


            var result = new ProjectCreateCommandResult { Id = project.Id };

            return OperationResult.Success(result);
        }
    }
}
