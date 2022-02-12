using ApplicationRegistry.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Domain.Entities.Network
{
    /*
    "name": "[DEV] VLAN: 001",
    "vlan": "001",
    "alias": null,
    "cidr": "192.168.11.0/27",
    "description": "Vlan for some mysterious machines",
    "env": "DEV",
    */
    public class VlanEntity: IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int? Number { get; set; }

        public string Alias { get; set; }

        public string Cidr { get; set; }

        public string Description { get; set; }

        public string RFC { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsVirtualDirectory { get; set; } = false;

        public string IpAsHexString { get; set; }

        public VlanEntity(Guid id, string name, DateTime createDate = default)
        {
            Id = id;
            Name = name;
            CreateDate = createDate == default ? DateTime.UtcNow : createDate;
        }
    }
}
