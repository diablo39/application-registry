﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationRegistry.Database.Entities
{
    public class SwaggerSpecificationOperationEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public Guid IdApplicationVersionSpecification { get; set; }

        public string Path { get; set; }

        public string OperationId { get; set; }

        public string HttpMethod { get; set; }

        public SwaggerApplicationVersionSpecificationEntity Specification { get; set; }
    }
}
