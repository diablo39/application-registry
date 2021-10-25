using ApplicationRegistry.Domain.Persistency;
using ApplicationRegistry.Domain.Repositories;
using ApplicationRegistry.Domain.Repositories.Network;
using ApplicationRegistry.Infrastructure.Domain.Persistency;
using ApplicationRegistry.Infrastructure.Repositories;
using ApplicationRegistry.Infrastructure.Repositories.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Infrastructure.UnitOfWork
{
    partial class ApplicationRegistryDatabaseContext
    {
        public IApplicationsRepository ApplicationsRepository => new ApplicationsRepository(this);

        public IEnvironmentRepository EnvironmentsRepository => new EnvironmentRepository(this);

        public ISystemsRepository SystemsRepository => new SystemsRepository(this);

        public INugetPackageRepository NugetPackageRepository => new NugetPackageRepository(this);

        public ILoadBalancerRepository LoadBalancerRepository => new LoadBalancerRepository(this);

        public IVlanRepository VlanRepository => new VlanRepository(this);

        public IRedisRepository RedisRepository => new RedisRepository(this);
    }
}
