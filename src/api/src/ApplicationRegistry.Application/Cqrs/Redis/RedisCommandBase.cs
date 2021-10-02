using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Application.Commands.Redis
{
    public abstract class RedisCommandBase
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string RedisDeploymentTypeId { get; set; }

        public string Description { get; set; }

        public string Requester { get; set; }

        public string RfcId { get; set; }

        public Guid? SentinelRedisId { get; set; }

        public string GroupId { get; set; }

        public string NetworkZone { get; set; }

        public string Password { get; set; }

        public string IdEnvironment { get; set; }

        public IEnumerable<RedisCommandBaseEndpoint> Endpoints { get; set; }
    }

    public interface RedisCommandBaseEndpoint
    {
        public Guid Id { get; set; }

        public string Host { get; set; }

        public string Port { get; set; }
    }
}
