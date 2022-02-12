using ApplicationRegistry.Database;
using System;

namespace ApplicationRegistry.Domain.Entities.Redis
{
    public class RedisDeploymentTypeEntity: IEntity<string>
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreateDate { get; set; }
    }
}
