using ApplicationRegistry.Application.Cqrs;
using ApplicationRegistry.Application.Services;
using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using FluentValidation;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Commands
{
    public class SystemCreateCommand : ICommand
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class SystemCreateCommandValidator : AbstractValidator<SystemCreateCommand>
    {
        public SystemCreateCommandValidator()
        {
            RuleFor(e => e.Name).IsName();
        }
    }

    public class SystemCreateCommandResult
    {
        public Guid Id { get; set; }
    }

    public class SystemCreateCommandHandler : ICommandHandler<SystemCreateCommand, SystemCreateCommandResult>
    {
        private readonly IUnitOfWork _context;
        private readonly IGuidGenerator _guidGenerator;

        public SystemCreateCommandHandler(IUnitOfWork context, IGuidGenerator guidGenerator)
        {
            _context = context;
            _guidGenerator = guidGenerator;
        }

        public async Task<OperationResult<SystemCreateCommandResult>> ExecuteAsync(SystemCreateCommand command)
        {
            var System = new SystemEntity(_guidGenerator.CreateNewSequentialGuid(), command.Name) { Description = command.Description };

            _context.Systems.Add(System);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            var result = new SystemCreateCommandResult { Id = System.Id };

            return OperationResult.Success(result);
        }
    }
}
