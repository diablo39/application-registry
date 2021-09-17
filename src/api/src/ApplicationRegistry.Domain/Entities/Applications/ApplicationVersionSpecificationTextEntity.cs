using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationRegistry.Database.Entities
{
    public class ApplicationVersionSpecificationTextEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        public string Specification { get; set; }
    }
}
