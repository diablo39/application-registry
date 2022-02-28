using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.CommandDecorators
{
    public class CatchExceptionsCommandHandlerDecorator<TIn, TOut> : ICommandHandler<TIn, TOut>
        where TIn : class, ICommand
    {
        readonly ICommandHandler<TIn, TOut> _next;
        readonly ILogger<CatchExceptionsCommandHandlerDecorator<TIn, TOut>> _logger;

        public CatchExceptionsCommandHandlerDecorator(ICommandHandler<TIn, TOut> next, ILogger<CatchExceptionsCommandHandlerDecorator<TIn, TOut>> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task<OperationResult<TOut>> ExecuteAsync(TIn command)
        {
            try
            {
                return await _next.ExecuteAsync(command);
            }
            catch(DomainException ex)
            {
                return OperationResult.BusinessError<TOut>(ex.ValidationErrors);
            }
            catch (ArgumentException ex)
            {
                return OperationResult.BusinessError<TOut>(new KeyValuePair<string, string>(ex.ParamName, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while invoking command handler: {handler}", _next.GetType());
                return OperationResult.ServerError<TOut>(ex);
            }
        }
    }
}
