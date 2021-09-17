using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Domain.Persistency;
using System;

namespace ApplicationRegistry.Infrastructure.Domain.Persistency
{
    public class ProjectsRepository : RepositoryBase<ProjectEntity>, IProjectsRepository
    {
        public ProjectsRepository(ApplicationRegistryDatabaseContext context) : base(context)
        {

        }

    }
}
