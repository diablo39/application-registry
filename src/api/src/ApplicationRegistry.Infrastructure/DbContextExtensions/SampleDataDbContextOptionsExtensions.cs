using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace ApplicationRegistry.Database.DbContextExtensions
{
    public static class SampleDataDbContextOptionsExtensions
    {
        public static DbContextOptionsBuilder UseSampleData(this DbContextOptionsBuilder optionsBuilder, bool useSampleData)
        {
            if (useSampleData)
            {
                ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(new SampleDataExtension());
            }

            return optionsBuilder;
        }

        public static DbContextOptionsBuilder<ApplicationRegistryDatabaseContext> UseSampleData(this DbContextOptionsBuilder<ApplicationRegistryDatabaseContext> optionsBuilder, bool useSampleData)
        {
            if (useSampleData)
            {
                ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(new SampleDataExtension());
            }
            return optionsBuilder;
        }
    }
}
