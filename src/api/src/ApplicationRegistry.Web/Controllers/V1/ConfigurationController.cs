using ApplicationRegistry.Application.Commands;
using ApplicationRegistry.Application.Queries;
using ApplicationRegistry.Application.Queries.ApplicationsList;
using ApplicationRegistry.Web.Areas.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationRegistry.CQRS.Abstraction;
using System;
using System.Linq;
using System.Threading.Tasks;
using ApplicationRegistry.Web.Models.Configuration;
using ApplicationRegistry.Web.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace ApplicationRegistry.Web.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]

    public class ConfigurationController : ControllerBase
    {
        // GET: api/<ApplicationsController>
        [HttpGet("frontendConfiguration", Name = "GetFrontendConfiguration")]
        [ProducesResponseType(typeof(FrontendConfigurationModel), StatusCodes.Status200OK)]
        [AllowAnonymous]
        
        public Task<IActionResult> Get([FromServices] IOptions<ApplicationConfiguration> applicationConfigurationOptions)
        {
            var applicationConfiguration = applicationConfigurationOptions.Value;

            var result = new FrontendConfigurationModel
            {
                Authentication = new OidcAuthenticationConfigurationModel
                {

                    Authority = applicationConfiguration.Authentication.Authority,
                    ClientId = applicationConfiguration.Authentication.ClientId,
                    PostLogoutRedirectUri = applicationConfiguration.Authentication.PostLogoutRedirectUri,
                    RedirectUri = applicationConfiguration.Authentication.RedirectUri,
                    ResponseType = applicationConfiguration.Authentication.ResponseType,
                    Scope = applicationConfiguration.Authentication.Scope,
                    SilentRedirectUri = applicationConfiguration.Authentication.SilentRedirectUri
                }
            };


            return Task.FromResult<IActionResult>(Ok(result));
        }
    }
}
