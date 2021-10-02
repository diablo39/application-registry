using ApplicationRegistry.Domain.Persistency;
using ApplicationRegistry.Domain.Repositories.Network;
using ApplicationRegistry.Infrastructure.Domain.Persistency;
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

        public IProjectsRepository ProjectsRepository => new ProjectsRepository(this);

        public INugetPackageRepository NugetPackageRepository => new NugetPackageRepository(this);

        public ILoadBalancerRepository LoadBalancerRepository => new LoadBalancerRepository(this);

        public IVlanRepository VlanRepository => new VlanRepository(this);
    }
}
