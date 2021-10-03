using ApplicationRegistry.Application.Cqrs.Network;
using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Domain.Entities.Network;
using FluentValidation;
using System;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Commands
{
    public class VlanCreateCommand : VlanCommandBase, ICommand
    {
        public Guid? Id { get; set; }
    }

    public class VlanCreateCommandValidator : VlanCommandValidatorBase<VlanCreateCommand>
    {
        public VlanCreateCommandValidator()
            : base()
        {
            RuleFor(e => e.Id).NotNull();
        }
    }

    public class VlanCreateCommandResult
    {
        public Guid Id { get; set; }
    }

    public class VlanCreateCommandHandler : ICommandHandler<VlanCreateCommand, VlanCreateCommandResult>
    {
        private readonly IUnitOfWork _context;

        public VlanCreateCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<OperationResult<VlanCreateCommandResult>> ExecuteAsync(VlanCreateCommand command)
        {
            var item = new VlanEntity(command.Id.Value, command.Name);

            item.CopyValuesFrom(command);

            _context.VlanRepository.Add(item);

            await _context.SaveChangesAsync();

            var result = new VlanCreateCommandResult { Id = item.Id };

            return OperationResult.Success(result);
        }
    }
}
