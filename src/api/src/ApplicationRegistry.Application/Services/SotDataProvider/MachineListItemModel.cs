using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ApplicationRegistry.Application.Services
{
    public class MachineListItemModel
    {
        public string Id { get { return FQDN; } }

        public string FQDN { get; set; }

        public string Name { get; set; }

        public string Env { get; set; }

        public string Group { get; set; }

        [JsonProperty("vcpu")]
        public string VCpu { get; set; }

        public string Memory { get; set; }

        [JsonProperty("operating-system")]
        public string Os { get; set; }       
        
        [JsonProperty("operating-system-distribution")]
        public string OsDistribution { get; set; }

        [JsonProperty("operating-system-version")]
        public string OsVersion { get; set; }

        public List<string> Vlans { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }


        [JsonProperty("vlans-list")]
        public List<Dictionary<string, string>> VlansList { get; set; }
    }
}
