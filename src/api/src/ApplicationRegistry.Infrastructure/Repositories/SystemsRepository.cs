using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Domain.Persistency;
using ApplicationRegistry.Infrastructure.UnitOfWork;
using System;

namespace ApplicationRegistry.Infrastructure.Domain.Persistency
{
    internal class SystemsRepository : RepositoryBase<SystemEntity>, ISystemsRepository
    {
        public SystemsRepository(ApplicationRegistryDatabaseContext context) : base(context)
        {

        }

    }
}
