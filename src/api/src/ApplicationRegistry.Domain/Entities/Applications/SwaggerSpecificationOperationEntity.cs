using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationRegistry.Database.Entities
{
    public class SwaggerSpecificationOperationEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreateDate { get; set; } = DateTime.UtcNow;

        public Guid IdApplicationVersionSpecification { get; set; }

        public string Path { get; set; }

        public string OperationId { get; set; }

        public string HttpMethod { get; set; }

        public ApplicationVersionSwaggerSpecificationEntity Specification { get; set; }
    }
}
