using ApplicationRegistry.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRegistry.Domain.Persistency
{
    public interface IApplicationsRepository : IRepository<ApplicationEntity>
    {
        ApplicationEntity Get(string idOrCode);
    }
}
