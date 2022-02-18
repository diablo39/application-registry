using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Domain.Exceptions;
using ApplicationRegistry.Domain.Persistency;
using ApplicationRegistry.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRegistry.Infrastructure.Domain.Persistency
{
    internal class ApplicationsRepository : RepositoryBase<ApplicationEntity>, IApplicationsRepository
    {
        private ApplicationRegistryDatabaseContext _context;

        public ApplicationsRepository(ApplicationRegistryDatabaseContext context)
            : base(context)
        {
            _context = context;
        }

        public override void Add(ApplicationEntity application)
        {
            if (CheckApplicationExists(application.Id, application.Code))
            {
                throw new DomainException(new KeyValuePair<string, string>("", $"Application with code: {application.Code } or id: {application.Id} already exists"));
            }

            if (!CheckSystemExists(application.IdSystem))
            {
                throw new DomainException(new KeyValuePair<string, string>(nameof(application.IdSystem), $"Project with id: {application.IdSystem} does not exist."));
            }

            _context.Applications.Add(application);
        }

        public ApplicationEntity Get(string idOrCode)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationEntity> GetWithEndpointsAsync(Guid id)
        {            
            var app =  await _context.Applications.Include(e=> e.Endpoints).FirstOrDefaultAsync(e => e.Id == id);

            return app;
        }

        private bool CheckApplicationExists(Guid id, string code)
        {
            return Exists(e => e.Id == id || e.Code == code);
        }

        private bool CheckSystemExists(Guid id)
        {
            return _context.SystemsRepository.Exists(e => e.Id == id);
        }
    }
}
