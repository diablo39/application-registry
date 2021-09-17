using Newtonsoft.Json;
using System;

namespace ApplicationRegistry.Application.Services
{
    public class LoadBalancerListItemModel
    {
        public string Id { get { return Vlan; } }

        public string RFC { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string Cidr { get; set; }

        public string Description { get; set; }

        public string Vlan { get; set; }

        public string Env { get; set; }

        [JsonProperty("machines-count")]
        public int MachinesCount { get; set; }
    }
}
