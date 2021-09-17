using System;
using System.Threading.Tasks;

namespace ApplicationRegistry.CQRS.Abstraction
{
    public interface IQueryHandler<T, R>
        where T : IQuery
    {
        Task<OperationResult<R>> ExecuteAsync(T query);
    }
}
