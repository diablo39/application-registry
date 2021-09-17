using Newtonsoft.Json;
using System;

namespace ApplicationRegistry.Application.Services
{
    public class VlanListItemModel
    {
        public string Id { get { return Cidr.Replace("/", "_"); } }

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
