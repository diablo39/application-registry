using ApplicationRegistry.CQRS.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.QueryDecorators
{
    public class OperationTypeQueryHandlerDecorator<TIn, TOut> : IQueryHandler<TIn, TOut>
        where TIn : class, IQuery
    {
        readonly IQueryHandler<TIn, TOut> _next;

        public OperationTypeQueryHandlerDecorator(IQueryHandler<TIn, TOut> next)
        {
            _next = next;
        }

        public async Task<OperationResult<TOut>> ExecuteAsync(TIn query)
        {

            var result = await _next.ExecuteAsync(query);

            result.OperationType = typeof(TIn);

            return result;

        }
    }
}
