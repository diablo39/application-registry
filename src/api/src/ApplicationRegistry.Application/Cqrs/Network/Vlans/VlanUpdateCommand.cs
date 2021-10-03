using ApplicationRegistry.Application.Cqrs.Network;
using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Commands
{
    public class VlanUpdateCommand : VlanCommandBase, ICommand
    {
        
    }

    public class VlanUpdateCommandValidator : VlanCommandValidatorBase<VlanUpdateCommand>
    {
        public VlanUpdateCommandValidator()
            : base()
        {

        }
    }

    public class VlanUpdateCommandResult
    {
        public Guid Id { get; set; }
    }

    public class VlanUpdateCommandHandler : ICommandHandler<VlanUpdateCommand, VlanUpdateCommandResult>
    {
        private readonly IUnitOfWork _context;

        public VlanUpdateCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<OperationResult<VlanUpdateCommandResult>> ExecuteAsync(VlanUpdateCommand command)
        {
            var item = _context.VlanRepository.Get(command.Id);

            if (item == null)
            {
                return OperationResult.BusinessError<VlanUpdateCommandResult>(new KeyValuePair<string, string>(nameof(command.Id), $"Vlan with id: { command.Id } not exists"));
            }

            item.CopyValuesFrom(command);

            await _context.SaveChangesAsync();

            var result = new VlanUpdateCommandResult { Id = command.Id };

            return OperationResult.Success(result);
        }
    }
}
