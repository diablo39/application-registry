using Microsoft.EntityFrameworkCore;
using System;

namespace ApplicationRegistry.Database.DbContextExtensions
{
    internal interface ISampleDataProvider
    {
        void SeedSampleData(ModelBuilder modelBuilder);

    }
}
