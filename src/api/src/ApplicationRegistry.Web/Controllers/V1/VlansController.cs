using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationRegistry.Application.Queries;
using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Web.Areas.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationRegistry.Web.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(ApiErrorModel), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ApiErrorModel), StatusCodes.Status500InternalServerError)]
    public class VlansController : ControllerBase
    {
        // GET: api/<ApplicationsController>
        [HttpGet]
        public async Task<IActionResult> Get(
            [FromServices] IQueryHandler<VlansListQuery, VlansListQueryResult> handler,
            [FromQuery] string sortBy = null,
            [FromQuery] bool? sortDesc = null,
            [FromQuery] int page = 1,
            [FromQuery] int itemsPerPage = -1)
        {
            VlansListQuery query = new VlansListQuery() { Page = page, ItemsPerPage = itemsPerPage, SortBy = sortBy, SortDesc = sortDesc };

            var result = await handler.ExecuteAsync(query).ToApiActionResult(HttpContext);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id, [FromServices] IQueryHandler<VlanDetailsQuery, object> handler)
        {
            var query = new VlanDetailsQuery() { Id = id };

            var result = await handler.ExecuteAsync(query).ToApiActionResult(HttpContext);

            return result;
        }
    }
}
