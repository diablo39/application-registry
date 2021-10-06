using ApplicationRegistry.Application.Services;
using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Domain.Entities.Network;
using FluentValidation;
using System;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Cqrs.Network.Vlans
{
    public class VlanCreateCommand : VlanCommandBase, ICommand
    {
    }

    public class VlanCreateCommandValidator : VlanCommandValidatorBase<VlanCreateCommand>
    {
        public VlanCreateCommandValidator()
            : base()
        {

        }
    }

    public class VlanCreateCommandResult
    {
        public Guid Id { get; set; }
    }

    public class VlanCreateCommandHandler : ICommandHandler<VlanCreateCommand, VlanCreateCommandResult>
    {
        private readonly IUnitOfWork _context;
        private readonly IGuidGenerator _guidGenerator;

        public VlanCreateCommandHandler(IUnitOfWork context, IGuidGenerator guidGenerator)
        {
            _context = context;
            _guidGenerator = guidGenerator;
        }

        public async Task<OperationResult<VlanCreateCommandResult>> ExecuteAsync(VlanCreateCommand command)
        {
            var item = new VlanEntity(_guidGenerator.CreateNewSequentialGuid(), command.Name);

            item.CopyValuesFrom(command);

            _context.VlanRepository.Add(item);

            await _context.SaveChangesAsync();

            var result = new VlanCreateCommandResult { Id = item.Id };

            return OperationResult.Success(result);
        }
    }
}
