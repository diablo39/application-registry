using System;
using System.Linq;

namespace ApplicationRegistry.CQRS.Abstraction
{
    public interface IServerErrorResult
    {
        Exception Exception { get; }
    }
}
