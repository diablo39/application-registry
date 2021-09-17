using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationRegistry.CQRS.Abstraction
{
    public interface IBusinessErrorResult
    {
        IEnumerable<KeyValuePair<string, string>> ValidationErrors { get; }
    }
}
