using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Domain.Exceptions;
using ApplicationRegistry.Domain.Persistency;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRegistry.Infrastructure.Domain.Persistency
{
    public class ApplicationsRepository : RepositoryBase<ApplicationEntity>, IApplicationsRepository
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

            if (!CheckProjectExists(application.IdProject))
            {
                throw new DomainException(new KeyValuePair<string, string>(nameof(application.IdProject), $"Project with id: {application.IdProject} does not exist."));
            }

            _context.Applications.Add(application);

            _context.Dependencies.Add(new ApplicationDependencyEntity(application.Name, application.Id));
        }

        public ApplicationEntity Get(string idOrCode)
        {
            throw new NotImplementedException();
        }

        private bool CheckApplicationExists(Guid id, string code)
        {
            return Exists(e => e.Id == id || e.Code == code);
        }

        private bool CheckProjectExists(Guid id)
        {
            return _context.Projects.Local.OfType<ProjectEntity>().Any(e => e.Id == id) || _context.Projects.Any(e => e.Id == id);
        }
    }
}
