using ApplicationRegistry.Domain.Entities.Network;
using ApplicationRegistry.Domain.Persistency;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Domain.Repositories.Network
{
    public interface ILoadBalancerRepository: IRepository<LoadBalancerEntity>
    {
    }
}
