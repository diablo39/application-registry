using ApplicationRegistry.Domain.Entities.Network;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Application.Cqrs.Network.Vlans
{
    public class VlanCommandBase
    {
        public string Name { get; set; }

        public int? Number { get; set; }

        public string Alias { get; set; }

        public string Cidr { get; set; }

        public string Description { get; set; }

        public string RFC { get; set; }
    }

    public abstract class VlanCommandValidatorBase<T> : AbstractValidator<T>
        where T : VlanCommandBase
    {
        protected VlanCommandValidatorBase()
        {
            RuleFor(e => e.Name).IsName();

            RuleFor(e => e.Number).GreaterThan(0);

            RuleFor(e => e.Alias).MaximumLength(400);

            RuleFor(e => e.Cidr).NotNull().NotEmpty().MaximumLength(40);

            RuleFor(e => e.Description).IsDescription();

            RuleFor(e => e.RFC).MaximumLength(400);
        }
    }

    public static class LoadBalancerSetter
    {
        public static void CopyValuesFrom(this VlanEntity item, VlanCommandBase command)
        {
            item.Alias = command.Alias;
            item.Cidr = command.Cidr;
            item.Description = command.Description;
            item.Name = command.Name;
            item.Number = command.Number;
            item.RFC = command.RFC;
        }
    }
}
