using ApplicationRegistry.Application.Commands;
using ApplicationRegistry.Application.Queries;
using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Web.Areas.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace ApplicationRegistry.Web.Areas.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(ApiErrorModel), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorModel), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ApiErrorModel), StatusCodes.Status500InternalServerError)]
    public class ApplicationVersionsController : ControllerBase
    {
        // GET: api/<ApplicationsController>
        [HttpGet()]
        [ProducesResponseType(typeof(ApplicationVersionListQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromServices] IQueryHandler<ApplicationVersionListQuery, ApplicationVersionListQueryResult> handler)
        {
            var query = new ApplicationVersionListQuery
            {

            };

            var result = await handler.ExecuteAsync(query).ToApiActionResult(HttpContext);

            return result;
        }

        // GET: api/<ApplicationsController>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApplicationVersionDetailsQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] Guid id, [FromServices] IQueryHandler<ApplicationVersionDetailsQuery, ApplicationVersionDetailsQueryResult> handler)
        {
            var query = new ApplicationVersionDetailsQuery
            {
                ApplicationVersionId = id,
            };
            var result = await handler.ExecuteAsync(query).ToApiActionResult(HttpContext);

            return result;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApplicationVersionCreateCommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] ApplicationVersionCreateCommand command, [FromServices] ICommandHandler<ApplicationVersionCreateCommand, ApplicationVersionCreateCommandResult> handler)
        {
            var result = await handler.ExecuteAsync(command).ToApiActionResult(HttpContext);

            return result;
        }

        [HttpGet("{id}/dependencies/nugets")]
        [ProducesResponseType(typeof(ApplicationVersionNugetDependencyListQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNugetDependencies([FromRoute] Guid id, [FromServices] IQueryHandler<ApplicationVersionNugetDependencyListQuery, ApplicationVersionNugetDependencyListQueryResult> handler)
        {
            var query = new ApplicationVersionNugetDependencyListQuery
            {
                ApplicationVersionId = id,
            };
            var result = await handler.ExecuteAsync(query).ToApiActionResult(HttpContext);

            return result;
        }

        [HttpGet("{id}/specifications/swagger")]
        public async Task<IActionResult> GetSwaggerSpecyfication([FromRoute] Guid id, [FromServices] IQueryHandler<SwaggerSpecyficationTextQuery, SwaggerSpecyficationTextQueryResult> handler)
        {
            var query = new SwaggerSpecyficationTextQuery
            {
                ApplicationVersionId = id
            };

            var result = await handler.ExecuteAsync(query).ToApiActionResult(HttpContext, success =>
            {
                if (success == null || success.Result == null) return new NotFoundObjectResult(new { });

                return Content(success.Result.Text, new MediaTypeHeaderValue(success.Result.ContentType).ToString());
            });

            return result;
        }

        [HttpGet("{id}/endpoints")]
        [ProducesResponseType(typeof(ApplicationEndpointListQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] Guid id, [FromServices] IQueryHandler<ApplicationEndpointListQuery, ApplicationEndpointListQueryResult> handler)
        {
            var query = new ApplicationEndpointListQuery
            {
                IdVersion = id
            };

            var result = await handler.ExecuteAsync(query).ToApiActionResult(HttpContext);

            return result;
        }
    }
}
