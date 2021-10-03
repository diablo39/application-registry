using ApplicationRegistry.Domain.Entities.Network;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Application.Commands.Network
{
    public abstract class LoadBalancerCommandBase
    {
        public string Name { get; set; }

        public string Ip { get; set; }

        public string Port { get; set; }

        public string Description { get; set; }

        public string Fqdn { get; set; }
    }

    public abstract class LoadBalancerCommandValidatorBase<T> : AbstractValidator<T>
        where T : LoadBalancerCommandBase
    {
        protected LoadBalancerCommandValidatorBase()
        {
            RuleFor(e => e.Name).NotNull().NotEmpty().MinimumLength(1).MaximumLength(250);
            RuleFor(e => e.Ip).NotNull().NotEmpty().MinimumLength(1).MaximumLength(250);
            RuleFor(e => e.Port).NotNull().NotEmpty().MinimumLength(1).MaximumLength(5);
            RuleFor(e => e.Description).NotNull().NotEmpty().MinimumLength(1);
            RuleFor(e => e.Fqdn).NotNull().NotEmpty().MinimumLength(1).MaximumLength(400);
        }
    }

    public static class LoadBalancerSetter
    {
        public static void CopyValuesFrom(this LoadBalancerEntity item, LoadBalancerCommandBase command)
        {
            item.Description = command.Description;
            item.Fqdn = command.Fqdn;
            item.Ip = command.Ip;
            item.Name = command.Name;
            item.Port = command.Port;
        }
    }
}
