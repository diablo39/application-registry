using ApplicationRegistry.Database;
using ApplicationRegistry.Domain.Entities.Network;
using ApplicationRegistry.Domain.Repositories.Network;
using ApplicationRegistry.Infrastructure.Domain.Persistency;
using ApplicationRegistry.Infrastructure.UnitOfWork;

namespace ApplicationRegistry.Infrastructure.Repositories.Network
{
    public class VlanRepository : RepositoryBase<VlanEntity>, IVlanRepository
    {

        public VlanRepository(ApplicationRegistryDatabaseContext context)
            : base(context)
        {

        }
    }
}
