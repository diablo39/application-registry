using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using ApplicationRegistry.CQRS.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationRegistry.Web.Areas.Api.Models
{
    public class ApiErrorModel : ValidationProblemDetails
    {
        public ApiErrorModel(ActionContext context)
        {
            Title = "Invalid arguments to the API";
            Detail = "The inputs supplied to the API are invalid";
            Status = 422;
            ConstructErrorMessages(context.ModelState);
            Type = "about:blank";
        }

        public ApiErrorModel(IBusinessErrorResult businessError, HttpContext context)
        {
            Title = "Invalid arguments to the API";
            Detail = "The inputs supplied to the API are invalid";
            Status = 422;

            var modelState = new ModelStateDictionary();

            foreach (var error in businessError.ValidationErrors)
            {
                modelState.AddModelError(error.Key, error.Value);
            }
            ConstructErrorMessages(modelState);
            Type = "about:blank";
        }

        public ApiErrorModel(IServerErrorResult serverError, HttpContext context)
        {
            Title = "Sever error";
            Detail = "Internal server occured.";
            Status = 500;
            Type = context.TraceIdentifier;
        }

        private void ConstructErrorMessages(ModelStateDictionary modelState)
        {
            foreach (var keyModelStatePair in modelState)
            {
                var key = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;
                if (errors != null && errors.Count > 0)
                {
                    if (errors.Count == 1)
                    {
                        var errorMessage = GetErrorMessage(errors[0]);
                        Errors.Add(key, new[] { errorMessage });
                    }
                    else
                    {
                        var errorMessages = new string[errors.Count];
                        for (var i = 0; i < errors.Count; i++)
                        {
                            errorMessages[i] = GetErrorMessage(errors[i]);
                        }
                        Errors.Add(key, errorMessages);
                    }
                }
            }
        }
        string GetErrorMessage(ModelError error)
        {
            return string.IsNullOrEmpty(error.ErrorMessage) ?
                "The input was not valid." :
            error.ErrorMessage;
        }
    }
}
