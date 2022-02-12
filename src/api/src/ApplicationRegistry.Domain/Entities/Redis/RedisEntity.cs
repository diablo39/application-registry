﻿using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationRegistry.Domain.Entities.Redis
{
    public class RedisEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreateDate { get; set; }

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

        public RedisDeploymentTypeEntity RedisDeploymentType { get; set; }

        public EnvironmentEntity Environment { get; set; }

        public IList<RedisEndpointEntity> Endpoints { get; set; }
    }  
}
