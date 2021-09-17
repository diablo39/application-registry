using ApplicationRegistry.CQRS.Abstraction;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.CommandDecorators
{
    public class ValidationCommandHandlerDecorator<TIn, TOut> : ICommandHandler<TIn, TOut>
        where TIn : class, ICommand
    {
        readonly ICommandHandler<TIn, TOut> _next;
        readonly IEnumerable<IValidator<TIn>> _validators;

        public ValidationCommandHandlerDecorator(ICommandHandler<TIn, TOut> next, IEnumerable<IValidator<TIn>> validators)
        {
            _validators = validators;
            _next = next;
        }

        public async Task<OperationResult<TOut>> ExecuteAsync(TIn command)
        {
            foreach (var validator in _validators)
            {
                var validationResult = validator.Validate(command);

                if (validationResult.IsValid) continue;

                var errors = validationResult.Errors.Select(e => new KeyValuePair<string, string>(e.PropertyName, e.ErrorMessage)).ToList();
         
                return OperationResult.BusinessError<TOut>(errors);
            }

            return await _next.ExecuteAsync(command);
        }
    }
}
