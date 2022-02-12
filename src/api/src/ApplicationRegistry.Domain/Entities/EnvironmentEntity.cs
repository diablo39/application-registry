using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationRegistry.Database.Entities
{
    public class EnvironmentEntity : IEntity<string>
    {
        [Required]
        [MaxLength(25)]
        public string Id { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreateDate { get; set; }
    }
}
