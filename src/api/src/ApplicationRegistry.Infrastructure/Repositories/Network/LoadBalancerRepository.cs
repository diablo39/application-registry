using ApplicationRegistry.Database;
using ApplicationRegistry.Domain.Entities.Network;
using ApplicationRegistry.Domain.Repositories.Network;
using ApplicationRegistry.Infrastructure.Domain.Persistency;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Infrastructure.Repositories.Network
{
    public class LoadBalancerRepository : RepositoryBase<LoadBalancerEntity>, ILoadBalancerRepository
    {

        public LoadBalancerRepository(ApplicationRegistryDatabaseContext context)
            : base(context)
        {

        }
    }
}
