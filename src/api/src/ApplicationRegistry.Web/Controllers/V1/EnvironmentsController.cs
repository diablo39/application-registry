using ApplicationRegistry.Application.Attributes;
using ApplicationRegistry.Application.Cqrs.Environments;
using ApplicationRegistry.CQRS.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationRegistry.Web.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvironmentsController : ControllerBase
    {
        [HttpGet(Name = "GetEnvironments")]
        [ProducesResponseType(typeof(EnvironmentsListQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] EnvironmentsListQuery query, [FromServices] IQueryHandler<EnvironmentsListQuery, EnvironmentsListQueryResult> handler)
        {
            var handlerResult = await handler.ExecuteAsync(query).ToApiActionResult(HttpContext);
            return handlerResult;
        }

        [HttpGet("{id}", Name = "GetEnvironmentDetails")]
        [ProducesResponseType(typeof(EnvironmentDetailsQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromServices] IQueryHandler<EnvironmentDetailsQuery, EnvironmentDetailsQueryResult> handler, string id)
        {
            var query = new EnvironmentDetailsQuery { Id = id };
            var handlerResult = await handler.ExecuteAsync(query).ToApiActionResult(HttpContext);
            return handlerResult;
        }

        [HttpPost(Name = "CreateEnvironment")]
        public async Task<IActionResult> Post([FromBody] EnvironmentCreateCommand command, [FromServices] ICommandHandler<EnvironmentCreateCommand, EnvironmentCreateCommandResult> handler)
        {
            var result = await handler.ExecuteAsync(command).ToApiActionResult(HttpContext);

            return result;
        }

        [HttpPut("{id}", Name = "UpdateEnvironment")]
        public async Task<IActionResult> Put(
            string id, 
            [FromBody][SwaggerIgnoreProperty(nameof(EnvironmentUpdateCommand.Id))] EnvironmentUpdateCommand command, 
            [FromServices] ICommandHandler<EnvironmentUpdateCommand, EnvironmentUpdateCommandResult> handler)
        {
            command.Id = id;

            var result = await handler.ExecuteAsync(command).ToApiActionResult(HttpContext);

            return result;
        }
    }
}
