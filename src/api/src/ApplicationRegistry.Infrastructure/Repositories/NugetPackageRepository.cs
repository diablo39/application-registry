using ApplicationRegistry.Domain.Entities;
using ApplicationRegistry.Domain.Persistency;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationRegistry.Infrastructure.Domain.Persistency
{
    internal class NugetPackageRepository : RepositoryBase<NugetPackageEntity>, INugetPackageRepository
    {
        private readonly DbContext _context;

        internal NugetPackageRepository(DbContext context) : base(context)
        {
            _context = context;
        }

        public NugetPackageEntity EnsureExists(string name, string version)
        {
            var result = this._set.FirstOrDefault(m => m.Name == name && m.Version == version);

            if (result != null) return result;

            result = new NugetPackageEntity(name, version);

            _context.Add(result);
            

            return result;
        }
    }
}
