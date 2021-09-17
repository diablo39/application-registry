using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Database.Entities
{
    public class ApplicationDependencyEntity : DependencyEntity
    {
        public Guid IdApplication { get; set; }

        internal ApplicationDependencyEntity() { }

        public ApplicationDependencyEntity(string name, Guid idApplication)
            : base(GetApplicationDependencyId(idApplication), name, DependencyTypes.Application)
        {
            IdApplication = idApplication;
        }

        public static string GetApplicationDependencyId(Guid idApplication)
        {
            return string.Concat(DependencyTypes.Application, ":", idApplication.ToString());
        }
    }
}
