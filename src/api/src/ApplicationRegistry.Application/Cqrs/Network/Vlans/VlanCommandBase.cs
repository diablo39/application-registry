using ApplicationRegistry.Domain.Entities.Network;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Application.Cqrs.Network
{
    public class VlanCommandBase
    {
        public string Name { get; set; }

        public int Number { get; set; }

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
