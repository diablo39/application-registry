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
    
    public class SystemUpdateCommand : ICommand
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class SystemUpdateCommandValidator : AbstractValidator<SystemUpdateCommand>
    {
        public SystemUpdateCommandValidator()
        {
            RuleFor(e => e.Id).NotNull();
            RuleFor(e => e.Name).NotEmpty();
        }
    }

    public class SystemUpdateCommandResult
    {
        public Guid Id { get; set; }
    }

    public class SystemUpdateCommandHandler : ICommandHandler<SystemUpdateCommand, SystemUpdateCommandResult>
    {
        private readonly IUnitOfWork _context;

        public SystemUpdateCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<OperationResult<SystemUpdateCommandResult>> ExecuteAsync(SystemUpdateCommand command)
        {
            var System = _context.ProjectsRepository.Get(command.Id);

            if (System == null)
            {
                System = new SystemEntity(command.Id ?? Guid.NewGuid(), command.Name);
                _context.ProjectsRepository.Add(System);
            }

            System.Name = command.Name;
            System.Description = command.Description;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return OperationResult.BusinessError<SystemUpdateCommandResult>(new Dictionary<string, string> { { "Id", $"System with id: {command.Id} already exists" } });
            }
            catch (Exception)
            {
                throw;
            }

            var result = new SystemUpdateCommandResult { Id = System.Id };

            return OperationResult.Success(result);
        }
    }
}
