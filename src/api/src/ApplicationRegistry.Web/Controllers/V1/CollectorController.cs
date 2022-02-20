using ApplicationRegistry.Application.CommandHandlers;
using ApplicationRegistry.Application.Commands;
using ApplicationRegistry.Web.Areas.Api.Models.Collector;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.AspNetCore.Http;
using ApplicationRegistry.Web.Areas.Api.Models;
using ApplicationRegistry.CQRS.Abstraction;
using System.Threading.Tasks;

namespace ApplicationRegistry.Web.Controllers.Api
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CollectorController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(
            [FromServices] CollectCliResultCommandHandler handler,
            [FromServices] ILogger<CollectorController> logger,
            [FromBody] CollectCliResultCommand command)
        {

            var result = handler.Handle(command);

            if (result.IsSuccess)
            {
                return Ok();
            }

            return BadRequest(result);
        }

        // POST api/<ApplicationsController>
        [HttpPost("logs")]
        [ProducesResponseType(typeof(LogErrorCommandResult), StatusCodes.Status202Accepted)]
        public async Task<IActionResult> Post([FromBody] LogErrorCommand command, [FromServices] ICommandHandler<LogErrorCommand, LogErrorCommandResult> handler)
        {
            var result = await handler.ExecuteAsync(command).ToApiActionResult(HttpContext, System.Net.HttpStatusCode.Accepted);

            return result;
        }

        [HttpPost("error")]
        public IActionResult Error([FromServices] ILogger<CollectorController> logger, [FromBody] CollectorError collectorError)
        {
            logger.LogError("Error reported by collector for application: [{0}] {1}: {2}. Error message: {3}", collectorError.IdEnvironment, collectorError.ApplicationCode, collectorError.Version, collectorError.ErrorMessage);

            return Ok();
        }
    }
}