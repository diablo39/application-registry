using System;
using System.Threading.Tasks;

namespace ApplicationRegistry.CQRS.Abstraction
{
    public interface ICommandHandler<TIn, TOut>
        where TIn : ICommand
    {
        Task<OperationResult<TOut>> ExecuteAsync(TIn command);
    }
}
