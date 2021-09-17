using ApplicationRegistry.Database.Entities;
using System;

namespace ApplicationRegistry.Domain.Persistency
{
    public interface IProjectsRepository : IRepository<ProjectEntity>
    {
    }
}
