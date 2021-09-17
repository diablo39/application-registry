using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.QueryDecorators
{
    public class CatchExceptionsQueryHandlerDecorator<TIn, TOut> : IQueryHandler<TIn, TOut>
        where TIn : class, IQuery
    {
        readonly IQueryHandler<TIn, TOut> _next;
        private readonly ILogger _logger;

        public CatchExceptionsQueryHandlerDecorator(IQueryHandler<TIn, TOut> next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger("QUERY HANDLER");
        }

        public async Task<OperationResult<TOut>> ExecuteAsync(TIn query)
        {
            try
            {
                return await _next.ExecuteAsync(query);
            }
            catch (DomainException ex)
            {
                return OperationResult.BusinessError<TOut>(ex.ValidationErrors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing query");
                return OperationResult.ServerError<TOut>(ex);
            }
        }
    }
}
