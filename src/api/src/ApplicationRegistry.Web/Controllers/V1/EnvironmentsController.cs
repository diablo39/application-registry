using ApplicationRegistry.Application.Cqrs.Environments;
using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Web.Areas.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationRegistry.Web.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(ApiErrorModel), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ApiErrorModel), StatusCodes.Status500InternalServerError)]
    public class EnvironmentsController : ControllerBase
    {
        [HttpGet()]
        [ProducesResponseType(typeof(EnvironmentsListQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] ListQueryParameters listQuery, [FromServices] IQueryHandler<EnvironmentsListQuery, EnvironmentsListQueryResult> handler)
        {
            EnvironmentsListQuery query = new EnvironmentsListQuery();
            query.AssignListQueryParameters(listQuery);
            var handlerResult = await handler.ExecuteAsync(query).ToApiActionResult(HttpContext);
            return handlerResult;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EnvironmentDetailsQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromServices] IQueryHandler<EnvironmentDetailsQuery, EnvironmentDetailsQueryResult> handler, string id)
        {
            EnvironmentDetailsQuery query = new EnvironmentDetailsQuery { Id = id };
            var handlerResult = await handler.ExecuteAsync(query).ToApiActionResult(HttpContext);
            return handlerResult;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EnvironmentCreateCommand command, [FromServices] ICommandHandler<EnvironmentCreateCommand, EnvironmentCreateCommandResult> handler)
        {
            var result = await handler.ExecuteAsync(command).ToApiActionResult(HttpContext);

            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] EnvironmentUpdateCommand command, [FromServices] ICommandHandler<EnvironmentUpdateCommand, EnvironmentUpdateCommandResult> handler)
        {
            command.Id = id;

            var result = await handler.ExecuteAsync(command).ToApiActionResult(HttpContext);

            return result;
        }
    }
}
