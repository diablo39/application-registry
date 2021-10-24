using ApplicationRegistry.Domain.Entities.Applications;
using ApplicationRegistry.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationRegistry.Database.Entities
{
    public class ApplicationEntity: IEntity<Guid>
    {
        readonly HashSet<ApplicationVersionEntity> _versions = new HashSet<ApplicationVersionEntity>();

        [Required]
        public Guid Id { get; private set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Column("IdSystem")]
        public Guid IdSystem { get; set; }


        public DateTime CreateDate { get; private set; }

        public string Description { get; set; }

        public string Owner { get; set; }

        public string RepositoryUrl { get; set; }

        public string BuildProcessUrls { get; set; }

        public string Framework { get; set; }

        #region Navigation properties

        public SystemEntity System { get; private set; }

        public IEnumerable<ApplicationVersionEntity> Versions => _versions;

        public IList<ApplicationEndpointEntity> Endpoints { get; set; }

        #endregion

        public ApplicationEntity(Guid id, string name, string code, Guid idSystem = default, DateTime createDate = default)
        {
            if (id == default) throw new DomainException(nameof(id), "");

            if (string.IsNullOrWhiteSpace(name)) throw new DomainException(nameof(name), "Name is required");

            if (string.IsNullOrWhiteSpace(code)) throw new DomainException(nameof(code), "Code is required");

            if (idSystem == default) idSystem = SystemEntity.UnasignedApplications;

            Id = id;
            Name = name;
            Code = code;
            IdSystem = idSystem;
            CreateDate = createDate == default ? DateTime.Now : createDate;

            Endpoints = new List<ApplicationEndpointEntity>();
        }
    }
}
