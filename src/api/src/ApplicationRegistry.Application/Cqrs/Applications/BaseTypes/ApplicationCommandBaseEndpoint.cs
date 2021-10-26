using System;

namespace ApplicationRegistry.Application.Commands
{
    public class ApplicationCommandBaseEndpoint
    {
        public string EnvironmentId { get; set; }

        public string Path { get; set; }

        public string Comment { get; set; }
    }

}
