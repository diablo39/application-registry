using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationRegistry.Database.Entities
{
    public class DependencyEntity : IEntity<string>
    {
        public string Id { get; set; }
        public DateTime CreateDate { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string IdDependencyType { get; set; }

        public string ExtraProperties { get; set; }

        [Display(Name = "Dependency type")]
        public DependencyTypeEntity DependencyType { get; set; }

        public List<DependencyVersionEntity> Versions { get; set; }

        public DependencyEntity() { }

        internal DependencyEntity(string name, string idDependencyType)
            : this()
        {
            CreateDate = DateTime.UtcNow;
            IdDependencyType = idDependencyType;
            Name = name;
            Id = string.Concat(idDependencyType, ":", name);
        }

        internal DependencyEntity(string id, string name, string idDependencyType)
            : this()
        {
            CreateDate = DateTime.UtcNow;
            IdDependencyType = idDependencyType;
            Name = name;
            Id = id;
        }

        public static DependencyEntity CreateDependencyEntity(string name, string idDependencyType)
        {
            return new DependencyEntity { CreateDate = DateTime.UtcNow, IdDependencyType = idDependencyType, Name = name, Id = string.Concat(idDependencyType, ":", name) };
        }

        public static DependencyEntity CreateApplicationDependency(string name, Guid idApplication)
        {
            return new ApplicationDependencyEntity(name, idApplication);
        }
    }
}
