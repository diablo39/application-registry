using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Domain.Entities.Applications
{
    public class ApplicationEndpointEntity : IEntity<Guid>
    {
        public Guid Id { get; private set; }

        public DateTime CreateDate { get; private set; }

        public Guid ApplicationId { get; set; }

        public string EnvironmentId { get; set; }

        public string Path { get; set; }

        public string Comment { get; set; }

        public EnvironmentEntity Environment { get; set; }

        public ApplicationEntity Application {get; set;}

        public ApplicationEndpointEntity(Guid id, string environmentId, Guid applicationId, string path, DateTime createDate = default)
        {
            if (id == default) throw new DomainException(nameof(id), "Id cant be default value for GUID");

            if (applicationId == default) throw new DomainException(nameof(applicationId), "ApplicationId cant be default value for GUID");
            
            if (string.IsNullOrWhiteSpace(environmentId)) throw new DomainException(nameof(environmentId), "EnvironmentId is required");

            if (string.IsNullOrWhiteSpace(path)) throw new DomainException(nameof(path), "Path is required");

            CreateDate = createDate == default ? DateTime.Now : createDate;

            Path = path;

            EnvironmentId = environmentId;
        }
    }
}
