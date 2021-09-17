using ApplicationRegistry.CQRS.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.CommandDecorators
{
    public class OperationTypeCommandHandlerDecorator<TIn, TOut> : ICommandHandler<TIn, TOut>
       where TIn : class, ICommand
    {
        readonly ICommandHandler<TIn, TOut> _next;

        public OperationTypeCommandHandlerDecorator(ICommandHandler<TIn, TOut> next)
        {
            _next = next;
        }

        public async Task<OperationResult<TOut>> ExecuteAsync(TIn command)
        {
            var result = await _next.ExecuteAsync(command);

            result.OperationType = typeof(TIn);

            return result;
        }
    }
}
