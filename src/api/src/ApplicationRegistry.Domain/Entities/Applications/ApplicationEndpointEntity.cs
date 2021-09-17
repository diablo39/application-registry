using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Domain.Entities.Applications
{
    public class ApplicationEndpointEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid ApplicationId { get; set; }

        public string EnvironmentId { get; set; }

        public string Path { get; set; }

        public string Comment { get; set; }

        public EnvironmentEntity Environment { get; set; }

        public ApplicationEntity Application {get; set;}
    }
}
