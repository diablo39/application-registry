using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using System;
using System.Threading.Tasks;
using FluentValidation;
using System.Collections.Generic;

namespace ApplicationRegistry.Application.Commands.Network
{
    public class LoadBalancerUpdateCommand : LoadBalancerCommandBase, ICommand
    {
        public Guid? Id { get; set; }

    }

    public class LoadBalancerUpdateCommandValidator : LoadBalancerCommandValidatorBase<LoadBalancerUpdateCommand>
    {
        public LoadBalancerUpdateCommandValidator()
            :base()
        {
            RuleFor(e => e.Id).NotNull();
        }
    }

    public class LoadBalancerUpdateCommandResult
    {
    }

    public class LoadBalancerUpdateCommandHandler : ICommandHandler<LoadBalancerUpdateCommand, LoadBalancerUpdateCommandResult>
    {
        private readonly IUnitOfWork _context;

        public LoadBalancerUpdateCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<OperationResult<LoadBalancerUpdateCommandResult>> ExecuteAsync(LoadBalancerUpdateCommand command)
        {
            var item = _context.LoadBalancerRepository.Get(command.Id);

            if (item == null)
            {
                return OperationResult.BusinessError<LoadBalancerUpdateCommandResult>(new KeyValuePair<string, string>(nameof(command.Id), $"Load balancer with id: { command.Id } not exists"));
            }

            item.CopyValuesFrom(command);

            await _context.SaveChangesAsync();

            var result = new LoadBalancerUpdateCommandResult();

            return OperationResult.Success(result);
        }
    }
}
