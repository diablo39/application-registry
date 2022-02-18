using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationRegistry.Database.Entities
{
    public class ApplicationVersionDependencyEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }

        [Required]
        public string IdDependency { get; set; }

        public string IdDependencyVersion { get; set; }

        public string ExtraProperties { get; set; }

        public DateTimeOffset CreateDate { get; set; }

        public Guid IdApplicationVersion { get; set; }

        //Navigation Properties
        public ApplicationVersionEntity ApplicationVersion { get; set; }

        public Guid? IdParent { get; set; }
   

        public ApplicationVersionDependencyEntity()
        {

        }

        public ApplicationVersionDependencyEntity(Guid idApplicationVersion, string idDependency, string idDependencyVersion = null)
        {
            Id = Guid.NewGuid();
            IdApplicationVersion = idApplicationVersion;
            IdDependency = idDependency;
            IdDependencyVersion = idDependencyVersion;
        }

        public static ApplicationVersionDependencyEntity Create(Guid idApplicationVersion, string idDependency, string idDependencyVersion = null)
        {
            return new ApplicationVersionDependencyEntity(idApplicationVersion, idDependency) { IdDependencyVersion = idDependencyVersion };
        }
    }
}
