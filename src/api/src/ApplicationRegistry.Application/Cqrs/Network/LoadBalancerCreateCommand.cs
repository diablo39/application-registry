using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Domain.Entities.Network;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Commands.Network
{
    public class LoadBalancerCreateCommand : LoadBalancerCommandBase, ICommand
    {
        public Guid Id { get; set; }

    }

    public class LoadBalancerCreateCommandValidator : LoadBalancerCommandValidatorBase<LoadBalancerCreateCommand>
    {
        public LoadBalancerCreateCommandValidator()
            :base()
        {

        }
    }

    public class LoadBalancerCreateCommandResult
    {
        public Guid Id { get; set; }
    }

    public class LoadBalancerCreateCommandHandler : ICommandHandler<LoadBalancerCreateCommand, LoadBalancerCreateCommandResult>
    {
        private readonly IUnitOfWork _context;

        public LoadBalancerCreateCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<OperationResult<LoadBalancerCreateCommandResult>> ExecuteAsync(LoadBalancerCreateCommand command)
        {
            var loadBalancer = new LoadBalancerEntity(command.Id, command.Name);

            loadBalancer.CopyValuesFrom(command);

            _context.LoadBalancerRepository.Add(loadBalancer);

            await _context.SaveChangesAsync();

            var result = new LoadBalancerCreateCommandResult { Id = loadBalancer.Id };

            return OperationResult.Success(result);
        }
    }
}
