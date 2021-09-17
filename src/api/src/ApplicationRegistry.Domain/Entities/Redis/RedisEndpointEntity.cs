using ApplicationRegistry.Database;
using System;

namespace ApplicationRegistry.Domain.Entities.Redis
{
    public class RedisEndpointEntityBase: IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid RedisId { get; set; }

        public DateTime CreateDate { get; set; }

        public string Host { get; set; }

        public string Port { get; set; }

    }

    public class RedisEndpointEntity: RedisEndpointEntityBase
    {

    }

    public class BulkLoadRedisEndpointEntity : RedisEndpointEntityBase
    {

    }
}
