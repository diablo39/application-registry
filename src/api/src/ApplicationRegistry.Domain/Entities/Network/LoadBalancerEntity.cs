using ApplicationRegistry.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationRegistry.Domain.Entities.Network
{
    public class LoadBalancerEntity : IEntity<Guid>
    {
        [Required]
        public Guid Id { get; private set; }

        public DateTime CreateDate { get; private set; }

        [Required]
        public string Name { get; set; }

        public string Ip { get; set; }

        public string Port { get; set; }

        public string Description { get; set; }

        public string Fqdn { get; set; }

        public LoadBalancerEntity(Guid id, string name, DateTime createDate = default)
        {
            Id = id;
            Name = name;
            CreateDate = createDate == default ? DateTime.UtcNow : createDate;
        }
    }
}
