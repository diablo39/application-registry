using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Domain.Persistency;
using ApplicationRegistry.Infrastructure.UnitOfWork;
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
