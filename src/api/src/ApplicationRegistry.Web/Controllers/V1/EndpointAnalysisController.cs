using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationRegistry.Application.Queries;
using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Web.Areas.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationRegistry.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(ApiErrorModel), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorModel), StatusCodes.Status422UnprocessableEntity)]
    public class EndpointAnalysisController : ControllerBase
    {
        [HttpGet("DrillDown")]
        [ProducesResponseType(typeof(EndpointAnalysisDrillDownQueryResult), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType(typeof(ApiErrorModel))]
        public async Task<IActionResult> GetDrillDown(
            [FromQuery] Guid idVersion,
            [FromQuery] String httpMethod,
            [FromQuery] String path,
            [FromServices] IQueryHandler<EndpointAnalysisDrillDownQuery, EndpointAnalysisDrillDownQueryResult> queryHandler)
        {
            EndpointAnalysisDrillDownQuery query = new EndpointAnalysisDrillDownQuery
            {
                HttpMethod = httpMethod,
                IdVersion = idVersion,
                Path = path
            };

            var result = await queryHandler.ExecuteAsync(query).ToApiActionResult(HttpContext);

            return result;
        }

        [HttpGet("DrillUp")]
        [ProducesResponseType(typeof(EndpointAnalysisDrillUpQueryResult), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType(typeof(ApiErrorModel))]
        public async Task<IActionResult> GetDrillUp(
           [FromQuery] Guid idVersion,
           [FromQuery] String httpMethod,
           [FromQuery] String path,
           [FromServices] IQueryHandler<EndpointAnalysisDrillUpQuery, EndpointAnalysisDrillUpQueryResult> queryHandler)
        {
            EndpointAnalysisDrillUpQuery query = new EndpointAnalysisDrillUpQuery
            {
                HttpMethod = httpMethod,
                IdVersion = idVersion,
                Path = path
            };

            var result = await queryHandler.ExecuteAsync(query).ToApiActionResult(HttpContext);

            return result;
        }

    }
}
