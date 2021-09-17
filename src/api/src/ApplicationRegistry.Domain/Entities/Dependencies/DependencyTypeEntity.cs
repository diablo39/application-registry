using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationRegistry.Database.Entities
{
    public class DependencyTypeEntity: IEntity<string>
    {
        public string Id { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        public string Name { get; set; }

        public bool CanBeAddedManualy { get; set; }


        public List<DependencyEntity> Dependencies { get; internal set; }
    }
}
