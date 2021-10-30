﻿using ApplicationRegistry.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationRegistry.Database.Entities
{
    public class SystemEntity: IEntity<Guid>
    {
        public static readonly Guid UnasignedApplications = Guid.Parse("{00000000-0000-0000-0000-000000000001}");

        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<ApplicationEntity> Applications { get; set; }

        public SystemEntity(Guid id, string name, DateTime createDate = default)
        {
            if (id == default) throw new DomainException(nameof(id), "");

            if (string.IsNullOrWhiteSpace(name)) throw new DomainException(nameof(name), "");

            Id = id;

            Name = name;

            CreateDate = createDate == default ? DateTime.Now : createDate;
        }
    }
}
