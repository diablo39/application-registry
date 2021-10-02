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

        public int Number { get; set; }

        public string Alias { get; set; }

        public string Cidr { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
