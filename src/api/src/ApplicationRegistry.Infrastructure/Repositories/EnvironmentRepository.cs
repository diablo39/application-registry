using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Domain.Persistency;
using ApplicationRegistry.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Infrastructure.Domain.Persistency
{
    public class EnvironmentRepository : RepositoryBase<EnvironmentEntity>, IEnvironmentRepository
    {
        public EnvironmentRepository(ApplicationRegistryDatabaseContext context) : base(context)
        {

        }

    }
}
