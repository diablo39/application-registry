using System;

namespace ApplicationRegistry.Application.Services
{
    public class SotDataProviderConfiguration
    {
        public string DataDirectory { get; set; }

        public int InvalidateCacheAfter { get; set; } = 5*60;

        public bool IsEnabled { get; set; } = true;
    }
}
