using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationRegistry.Database.Entities
{
    public class SpecificationTypeEntity : IEntity<String>
    {
        public const string Swagger = "Swagger";

        public string Id { get; set; }

        public DateTimeOffset CreateDate { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
