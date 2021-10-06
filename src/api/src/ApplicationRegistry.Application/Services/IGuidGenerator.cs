using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Application.Services
{
    public interface IGuidGenerator
    {
        Guid CreateNewSequentialGuid();
    }
}
