using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Web.Areas.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public static class OperationResultExtensions
    {
        public static async Task<IActionResult> ToApiActionResult<R>(this Task<OperationResult<R>> resultTask, 
            HttpContext context, 
            Func<ISuccessResult<R>, IActionResult> mapSuccess)
        {
            var result = await resultTask;

            context.Response.Headers.Add("Operation-Name", result.OperationType?.Name ?? "Not supported");

            switch (result)
            {
                case ISuccessResult<R> success:
                    if (mapSuccess != null) return mapSuccess(success);
                    if (success == null || success.Result == null) return new NotFoundObjectResult(new { });
                    return new ObjectResult(success.Result);
                case IBusinessErrorResult businessError:
                    var businessErrorModel = new ApiErrorModel(businessError, context);
                    return new UnprocessableEntityObjectResult(businessErrorModel);
                case IServerErrorResult serverError:
                    var serverErrorModel = new ApiErrorModel(serverError, context);
                    return new ObjectResult(serverErrorModel) { StatusCode = 500 };
                default:
                    return new RedirectResult("/Home/Error");
            }
        }

        public static async Task<IActionResult> ToApiActionResult<R>(this Task<OperationResult<R>> resultTask, 
            HttpContext context, 
            HttpStatusCode successCode = HttpStatusCode.OK)
        {
            var result = await resultTask;

            context.Response.Headers.Add("Operation-Name", result.OperationType?.Name ?? "Not supported");

            switch (result)
            {
                case ISuccessResult<R> success:
                    if (success == null || success.Result == null) return new NotFoundObjectResult(new { });
                    return new ObjectResult(success.Result) { StatusCode = (int)successCode };
                case IBusinessErrorResult businessError:
                    var businessErrorModel = new ApiErrorModel(businessError, context);
                    return new UnprocessableEntityObjectResult(businessErrorModel);
                case IServerErrorResult serverError:
                    var serverErrorModel = new ApiErrorModel(serverError, context);
                    return new ObjectResult(serverErrorModel) { StatusCode = 500 };
                default:
                    return new RedirectResult("/Home/Error");
            }
        }
    }
}
