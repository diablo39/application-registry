using System;
using System.Linq;

namespace ApplicationRegistry.CQRS.Abstraction
{
    public interface ISuccessResult<TOut>
    {
        TOut Result { get; }
    }
}
