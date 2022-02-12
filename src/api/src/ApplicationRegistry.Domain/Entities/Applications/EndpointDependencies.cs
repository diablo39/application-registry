using ApplicationRegistry.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Domain.Entities
{
    public class EndpointDependencies : IEntity<HierarchyId>
    {
        public HierarchyId Id { get; set; }

        public string EnvironmentId { get; set; }

        public string ApplicationCode { get; set; }

        public string HttpMethod { get; set; }

        public string Path { get; set; }

        public string HierarchyChecksum { get; set; }

        public string ProcessName { get; set; }

        public DateTimeOffset CreateDate { get; set; }

    }
}
