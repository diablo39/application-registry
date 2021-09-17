using System;
using System.Collections.Generic;

namespace ApplicationRegistry.CQRS.Abstraction
{
    public interface ICollectionQueryResult<T>
    {
        int TotalCount { get; }

        IEnumerable<T> Items { get; }
    }
}
