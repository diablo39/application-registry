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
    public class LoadBalancerCreateCommand : ICommand
    {
        public Guid Id { get; private set; }

        public DateTime CreateDate { get; private set; }

        public string Name { get; set; }

        public string Ip { get; set; }

        public string Port { get; set; }

        public string Description { get; set; }

        public string Fqdn { get; set; }
    }

    public class LoadBalancerCreateCommandValidator : AbstractValidator<LoadBalancerCreateCommand>
    {
        public LoadBalancerCreateCommandValidator()
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
            var loadBalancer = new LoadBalancerEntity(command.Id, command.Name)
            {
                Description = command.Description,
                Fqdn = command.Fqdn,
                Ip = command.Ip,
                Name = command.Name,
                Port = command.Port                
            };
            _context.LoadBalancerRepository.Add(loadBalancer);

            await _context.SaveChangesAsync();
            var result = new LoadBalancerCreateCommandResult { Id = loadBalancer.Id };

            return OperationResult.Success(result);
        }
    }
}
