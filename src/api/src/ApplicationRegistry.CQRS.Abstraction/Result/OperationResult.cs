using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ApplicationRegistry.CQRS.Abstraction
{
    public static class OperationResult
    {
        public static OperationResult<TOut> Success<TOut>(TOut data = default)
        {
            return new SuccessOperationResult<TOut>(data);
        }

        public static OperationResult<TOut> BusinessError<TOut>(IEnumerable<KeyValuePair<string, string>> errors)
        {
            return new BusinessErrorOperationResult<TOut>(errors);
        }
        public static OperationResult<TOut> BusinessError<TOut>(params KeyValuePair<string, string>[] errors)
        {
            return new BusinessErrorOperationResult<TOut>(errors);
        }

        public static OperationResult<TOut> ServerError<TOut>(Exception ex)
        {
            return new ServerErrorOperationResult<TOut>(ex);
        }

        internal class SuccessOperationResult<TOut> : OperationResult<TOut>, ISuccessResult<TOut>
        {
            public SuccessOperationResult(TOut result)
            {
                Result = result;
            }
        }

        internal class ServerErrorOperationResult<TOut> : OperationResult<TOut>, IServerErrorResult
        {
            public ServerErrorOperationResult(Exception ex)
            {
                Exception = ex;
            }
        }

        internal class BusinessErrorOperationResult<TOut> : OperationResult<TOut>, IBusinessErrorResult
        {
            public BusinessErrorOperationResult(IEnumerable<KeyValuePair<string, string>> errors)
            {
                ValidationErrors = new ReadOnlyCollection<KeyValuePair<string, string>>(errors.ToList());
            }
        }
    }
}
