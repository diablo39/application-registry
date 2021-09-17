using System;
using System.Net;

namespace ApplicationRegistry.UnitTests.SampleDataGenerator.DataModel
{
    public class VLan
    {
        private IPNetwork _ipnetwork;
        private IPAddressCollection _ipAddresses;

        public string Name { get { return $"[{Env}] VLAN: {Vlan}"; } }

        public string Vlan { get; set; }

        public string Alias { get; set; }

        public string Cidr { get; }

        public string Description { get { return "Vlan for some mysterious machines"; } }

        public string Env { get; set; }

        public VLan(string cidr)
        {
            Cidr = cidr;
            _ipnetwork = IPNetwork.Parse(Cidr);

            _ipAddresses = _ipnetwork.ListIPAddress(FilterEnum.Usable);
        }

        public MachineNetworkInterface CreateNetworkInterface()
        {
            var result = new MachineNetworkInterface
            {
                Name = "ens302",
                Ipv4 = _ipAddresses.MoveNext() ? _ipAddresses.Current.ToString() : throw new Exception("Out of ip addresses"),

                NetMask = _ipnetwork.Netmask.ToString(),
                Network = _ipnetwork.Network.ToString()
            };
            return result;
        }
    }
}
