using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationRegistry.Database.Entities
{
    public enum ValidationStatus
    {
        OK = 0 ,
        Warning = 1,
        Error = 2
    }

    public class DependencyVersionEntity : IEntity<string>
    {
        public string Id { get; set; }

        public DateTimeOffset CreateDate { get; set; }

        [Required]
        public string IdDependency { get; set; }

        [Required]
        public string Version { get; set; }

        public ValidationStatus ValidationStatus { get; set; }

    }
}
