using ApplicationRegistry.Domain.Entities;
using System;
using System.Linq;

namespace ApplicationRegistry.Domain.Persistency
{
    public interface INugetPackageRepository
    {
        NugetPackageEntity EnsureExists(string name, string version);
    }
}
