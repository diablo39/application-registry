using ApplicationRegistry.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace ApplicationRegistry.Infrastructure.ApplicationSevices
{
    class GuidGenerator : IGuidGenerator
    {
        private static SequentialGuidValueGenerator _generator = new SequentialGuidValueGenerator();
        public Guid CreateNewSequentialGuid()
        {
            Guid result = _generator.Next(default);
            return result;
        }
    }
}
