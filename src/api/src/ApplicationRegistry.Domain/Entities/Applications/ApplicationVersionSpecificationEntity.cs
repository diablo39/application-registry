using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationRegistry.Database.Entities
{
    public class SwaggerApplicationVersionSpecificationEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid IdApplicationVersion { get; set; }

        public string ContentType { get; set; }

        public DateTimeOffset CreateDate { get; set; }

        [Required]
        public string SpecificationTextHash { get; set; }

        [Required]
        public string Code { get; set; }

        public string Specification { get; set; }

        // navigation properties
        public ApplicationVersionEntity ApplicationVersion { get; set; }

        public string OperationsStringified { get; set; }

        public SwaggerApplicationVersionSpecificationEntity()
        {

        }
    }
}
