using ApplicationRegistry.UnitTests.SampleDataGenerator.DataModel;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ApplicationRegistry.UnitTests.SampleDataGenerator
{


    public class SampleDataGeneror
    {
        List<FirewallRule> _firewallRules = new List<FirewallRule>();

        List<Environment> _environments = new List<Environment>();

        public List<VLan> _vlans = new List<VLan>();

        public List<Machine> _machines = new List<Machine>();

        public void AddEnvironment(string name, string description = "")
        {
            _environments.Add(new Environment { Name = name, Description = description });
        }

        public void AddVlan(VLan vlan)
        {
            _vlans.Add(vlan);
        }

        public void AddVlans(IEnumerable<VLan> vlans)
        {
            _vlans.AddRange(vlans);
        }

        public void AddMachines(int count, string name, string env, VLan[] vlans, string operatingSystemClass, string operatingSystem, int vcpu, int memory, string systemVersion = "18.10")
        {
            for (int i = 0; i < count; i++)
            {

                var machine = new Machine
                {
                    Name = name + i.ToString("00"),
                    Env = env,
                    OperatingSystemDistribution = operatingSystem,
                    OperatingSystemClass = operatingSystemClass,
                    OperatingSystemVersion = systemVersion,
                    OperatingSystem = operatingSystem + " " + systemVersion,
                    vcpu = vcpu,
                    memory = memory,
                    Description = "Server for tasks :)",
                    FQDN = name + i.ToString("00") + ".cmdb-gig.localdomain"
                };

                if (vlans != null)
                {
                    for (int j = 0; j < vlans.Length; j++)
                    {
                        var networkInterface = vlans[j].CreateNetworkInterface();
                        networkInterface.Name = networkInterface.Name.Substring(0, networkInterface.Name.Length - 2) + (j * 10 + 2).ToString();
                        machine.NetworkInterfaces.Add(networkInterface);
                    }
                }

                machine.DataVolumes.Add(new MachineDataVolume { Device = "sda", FsType = "ext4", Size = "10GB", Mount = "/" });
                machine.DataVolumes.Add(new MachineDataVolume { Device = "sda", FsType = "ext4", Size = "10GB", Mount = "/tmp" });
                machine.DataVolumes.Add(new MachineDataVolume { Device = "sda", FsType = "ext4", Size = "10GB", Mount = "/var" });
                machine.DataVolumes.Add(new MachineDataVolume { Device = "sda", FsType = "ext4", Size = "10GB", Mount = "/var/log" });
                machine.DataVolumes.Add(new MachineDataVolume { Device = "sda", FsType = "ext4", Size = "10GB", Mount = "/opt" });
                machine.DataVolumes.Add(new MachineDataVolume { Device = "sda", FsType = "ext4", Size = "10GB", Mount = "/data" });
                _machines.Add(machine);
            }
        }

        public void Generate(string path)
        {
            if (!path.EndsWith("/") || !path.EndsWith("\\"))
            {
                path += Path.DirectorySeparatorChar;
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            SaveData(_environments, path, "env");
            SaveData(_vlans, path, "vlans");
            SaveData(_machines, path, "machines");
            SaveData(_firewallRules, path, "firewall-rules");
        }

        private ISerializer GetSerializer()
        {
            var serializer = new SerializerBuilder()
                .WithNamingConvention(new HyphenatedNamingConvention())
                .Build();
            return serializer;
        }

        private void SaveData<T>(T data, string path, string dataType)
        {
            var fileName = dataType + ".yaml";
            var document = new Dictionary<string, object>() { { dataType, data } };

            var serializer = GetSerializer();

            var yaml = serializer.Serialize(document);

            var filePath = Path.Combine(path, fileName);

            File.WriteAllText(filePath, yaml);
        }

        internal void AddFirewallRules(List<FirewallRule> list)
        {
            _firewallRules.AddRange(list);
        }
    }

}
