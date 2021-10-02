using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace ApplicationRegistry.Application.Services
{
    public class SotDataProvider : IDisposable
    {
        private readonly Timer _timer;


        private JObject _cache = null;


        private JArray _vlanCache = null;


        private readonly IOptions<SotDataProviderConfiguration> _configuration;


        private bool _disposedValue;


        private readonly ILogger<SotDataProvider> _logger;


        public SotDataProvider(IOptions<SotDataProviderConfiguration> configuration, ILogger<SotDataProvider> logger)
        {
            _logger = logger;
            _configuration = configuration;
            if (_configuration.Value.IsEnabled)
            {
                _timer = new Timer(new TimerCallback(TimerTick));
                _timer.Change(0, configuration.Value.InvalidateCacheAfter * 1000);
            }
        }

        public Task<List<VlanListItemModel>> GetVlansAsync()
        {
            var collectionName = "vlans";

            return GetList<VlanListItemModel>(collectionName);
        }

        public Task<object> GetVlanDetailsAsync(string id)
        {
            var cidr = id.Replace("_", "/");

            var result = _vlanCache.FirstOrDefault(e => e.Value<string>("cidr") == cidr);

            return Task.FromResult<object>(result);
        }


        public Task<List<MachineListItemModel>> GetMachinesAsync()
        {
            var collectionName = "machines";

            return GetList<MachineListItemModel>(collectionName);
        }


        public Task<object> GetMachineDetailsAsync(string id)
        {

            var dataDir = "machines";

            return GetDetailFileContent(id, dataDir);
        }

        public Task<List<object>> GetFirewallRulesAsync()
        {

            var collectionName = "firewall-rules";

            return GetList<object>(collectionName);
        }

        internal Task<object> GetFirewallRuleDetailsAsync(string id)
        {
            var dataDir = "firewall-rules";

            return GetDetailFileContent(id, dataDir);
        }

        private string GetDataPath()
        {
            var path = _configuration.Value.DataDirectory;

            var dataPath = Path.Combine(Directory.GetCurrentDirectory(), path);
            return dataPath;
        }

        private Task<List<T>> GetList<T>(string collectionName)
        {
            JObject cmdb = GetCmdbFile();

            var vlansJson = cmdb[collectionName];

            var vlans = vlansJson.ToObject<List<T>>();

            return Task.FromResult(vlans);
        }

        private JObject GetCmdbFile()
        {
            if (_cache != null)
                return _cache;

            string dataPath = GetDataPath();

            var cmdbPath = Path.Combine(dataPath, "cmdb.json");

            var cmdbFileContent = File.ReadAllText(cmdbPath);

            var cmdb = JObject.Parse(cmdbFileContent);

            _cache = cmdb;

            return cmdb;
        }

        private Task<object> GetDetailFileContent(string id, string dataDir)
        {
            string dataPath = GetDataPath();

            var vlanPath = Path.Combine(dataPath, dataDir, id + ".json");

            if (!File.Exists(vlanPath))
                return null;

            try
            {
                using (var textReader = new JsonTextReader(new StreamReader(vlanPath)))
                {
                    var vlanContent = File.ReadAllText(vlanPath);

                    var result = JsonSerializer.CreateDefault().Deserialize<object>(textReader);

                    return Task.FromResult(result);
                }
            }
            catch
            {

                return null;
            }
        }


        private void TimerTick(object state)
        {
            string dataPath = GetDataPath();

            var cmdbPath = Path.Combine(dataPath, "cmdb.json");

            var cmdbFileContent = File.ReadAllText(cmdbPath);

            var cmdb = JObject.Parse(cmdbFileContent);

            _cache = cmdb;

            var vlanCmdbPath = Path.Combine(dataPath, "vlans.json");

            var vlanCmdbFileContent = File.ReadAllText(vlanCmdbPath);

            var vlans = JArray.Parse(vlanCmdbFileContent);

            _vlanCache = vlans;

            _logger.Log(LogLevel.Information, "SoT Data  refreshed");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (_timer != null)
                        _timer.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~SotDataProvider()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


    }
}
