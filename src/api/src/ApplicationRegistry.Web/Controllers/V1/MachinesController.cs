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
    [Route("api/[controller]")]
    [ApiController]
    public class MachinesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(
            [FromServices] IQueryHandler<MachineListQuery, MachineListQueryResult> handler,
            [FromQuery] string sortBy = null,
            [FromQuery] bool? sortDesc = null,
            [FromQuery] int page = 1,
            [FromQuery] int itemsPerPage = -1)
        {
            var query = new MachineListQuery() { Page = page, ItemsPerPage = itemsPerPage, SortBy = sortBy, SortDesc = sortDesc };

            var result = await handler.ExecuteAsync(query).ToApiActionResult(HttpContext);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id, [FromServices] IQueryHandler<MachineDetailsQuery, object> handler)
        {
            var query = new MachineDetailsQuery() { Id = id };

            var result = await handler.ExecuteAsync(query).ToApiActionResult(HttpContext);

            return result;
        }
    }
}
