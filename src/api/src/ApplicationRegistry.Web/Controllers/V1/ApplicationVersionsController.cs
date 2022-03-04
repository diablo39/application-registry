using ApplicationRegistry.Application.Cqrs.ApplicationVersions;
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
    public class ApplicationVersionsController : ControllerBase
    {
        // GET: api/<ApplicationsController>
        [HttpGet("{id}", Name = "GetApplicationVersion")]
        [ProducesResponseType(typeof(ApplicationVersionDetailsQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(
            [FromRoute] Guid id,
            [FromServices] IQueryHandler<ApplicationVersionDetailsQuery, ApplicationVersionDetailsQueryResult> handler)
        {
            var query = new ApplicationVersionDetailsQuery
            {
                ApplicationVersionId = id,
            };
            var result = await handler.ExecuteAsync(query).ToApiActionResultAsync(HttpContext);

            return result;
        }

        [HttpPost(Name = nameof(ApplicationVersionsController.CreateApplicationVersion))]
        [ProducesResponseType(typeof(ApplicationVersionCreateCommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateApplicationVersion([FromBody] ApplicationVersionCreateCommand command, [FromServices] ICommandHandler<ApplicationVersionCreateCommand, ApplicationVersionCreateCommandResult> handler)
        {
            var result = await handler.ExecuteAsync(command).ToApiActionResultAsync(HttpContext);

            return result;
        }

        [HttpGet("{id}/dependencies/nugets", Name = nameof(ApplicationVersionsController.GetNugetDependencies))]
        [ProducesResponseType(typeof(ApplicationVersionNugetDependencyListQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNugetDependencies([FromRoute] Guid id, [FromServices] IQueryHandler<ApplicationVersionNugetDependencyListQuery, ApplicationVersionNugetDependencyListQueryResult> handler)
        {
            var query = new ApplicationVersionNugetDependencyListQuery
            {
                ApplicationVersionId = id,
            };
            var result = await handler.ExecuteAsync(query).ToApiActionResultAsync(HttpContext);

            return result;
        }

        [HttpGet("{id}/specifications/swagger", Name = nameof(ApplicationVersionsController.GetSwaggerSpecyfication))]
        [Obsolete]
        public async Task<IActionResult> GetSwaggerSpecyfication([FromRoute] Guid id, [FromServices] IQueryHandler<SwaggerSpecyficationTextQuery, SwaggerSpecyficationTextQueryResult> handler)
        {
            var query = new SwaggerSpecyficationTextQuery
            {
                ApplicationVersionId = id
            };

            var result = await handler.ExecuteAsync(query).ToApiActionResultAsync(HttpContext, success =>
            {
                if (success == null || success.Result == null) return new NotFoundObjectResult(new { });

                return Content(success.Result.Text, new MediaTypeHeaderValue(success.Result.ContentType).ToString());
            });

            return result;
        }

        [HttpGet("{id}/specifications/swaggers", Name = nameof(ApplicationVersionsController.GetSwaggers))]
        public async Task<IActionResult> GetSwaggers([FromRoute] Guid id, [FromServices] IQueryHandler<ApplicationVersionSwaggerListQuery, ApplicationVersionSwaggerListQueryResult> handler)
        {
            var query = new ApplicationVersionSwaggerListQuery
            {
                ApplicationVersionId = id
            };

            var result = await handler.ExecuteAsync(query).ToApiActionResultAsync(HttpContext);

            return result;
        }

        [HttpGet("{id}/specifications/swaggers/{idApplicationVersion}/text", Name = nameof(ApplicationVersionsController.GetSwaggerSpecyficationText))]
        public async Task<IActionResult> GetSwaggerSpecyficationText([FromRoute] Guid id, [FromRoute] Guid idApplicationVersion, [FromServices] IQueryHandler<ApplicationVersionSwaggerSpecificationTextQueryQuery, ApplicationVersionSwaggerSpecificationTextQueryQueryResult> handler)
        {
            var query = new ApplicationVersionSwaggerSpecificationTextQueryQuery
            {
                ApplicationVersionId = idApplicationVersion,
                Id = id,
            };

            var result = await handler.ExecuteAsync(query).ToApiActionResultAsync(HttpContext, success =>
            {
                if (success == null || success.Result == null) return new NotFoundObjectResult(new { });

                return Content(success.Result.Text, new MediaTypeHeaderValue(success.Result.ContentType).ToString());
            });

            return result;
        }

        [HttpGet("{id}/endpoints", Name = nameof(ApplicationVersionsController.GetEndpoints))]
        [ProducesResponseType(typeof(ApplicationVersionEndpointListQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEndpoints([FromRoute] Guid id, [FromServices] IQueryHandler<ApplicationVersionEndpointListQuery, ApplicationVersionEndpointListQueryResult> handler)
        {
            var query = new ApplicationVersionEndpointListQuery
            {
                IdVersion = id
            };

            var result = await handler.ExecuteAsync(query).ToApiActionResultAsync(HttpContext);

            return result;
        }
    }
}