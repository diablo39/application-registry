using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ApplicationRegistry.Application.Attributes;
using ApplicationRegistry.Application.Cqrs.Network.Vlans;
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
        // GET: api/vlans
        [HttpGet(Name = "GetVlanList")]
        [ProducesResponseType(typeof(VlanListQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(
            [FromServices] IQueryHandler<VlanListQuery, VlanListQueryResult> handler,
            [FromQuery] string sortBy = null,
            [FromQuery] bool? sortDesc = null,
            [FromQuery] int page = 1,
            [FromQuery] int itemsPerPage = -1)
        {
            var query = new VlanListQuery() { Page = page, ItemsPerPage = itemsPerPage, SortBy = sortBy, SortDesc = sortDesc };

            var result = await handler.ExecuteAsync(query).ToApiActionResult(HttpContext);

            return result;
        }

        [HttpGet("{id}", Name = "GetVlanDetails")]
        [ProducesResponseType(typeof(VlanDetailsQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] Guid id, [FromServices] IQueryHandler<VlanDetailsQuery, VlanDetailsQueryResult> handler)
        {
            var query = new VlanDetailsQuery() { Id = id };

            var result = await handler.ExecuteAsync(query).ToApiActionResult(HttpContext);

            return result;
        }

        // POST api/<vlans>
        [HttpPost(Name = "CreateVlan")]
        [ProducesResponseType(typeof(VlanCreateCommandResult), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] VlanCreateCommand command, [FromServices] ICommandHandler<VlanCreateCommand, VlanCreateCommandResult> handler)
        {
            var result = await handler.ExecuteAsync(command).ToApiActionResult(HttpContext, HttpStatusCode.Created);

            return result;
        }

        [HttpPut("{id}", Name = "UpdateVlan")]
        [ProducesResponseType(typeof(VlanUpdateCommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put([Required] Guid? id,
            [FromBody][SwaggerIgnoreProperty("Id")] VlanUpdateCommand command,
            [FromServices] ICommandHandler<VlanUpdateCommand, VlanUpdateCommandResult> handler)
        {
            command.Id = id.Value;

            var result = await handler.ExecuteAsync(command).ToApiActionResult(HttpContext);

            return result;
        }

        // GET: api/vlans
        [HttpGet("{id}/children", Name = "GetVlanChildrenList")]
        [ProducesResponseType(typeof(VlanChildrenListQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetChildren(
            [Required] Guid? id,
            [FromServices] IQueryHandler<VlanChildrenListQuery, VlanChildrenListQueryResult> handler,
            [FromQuery] string sortBy = null,
            [FromQuery] bool? sortDesc = null,
            [FromQuery] int page = 1,
            [FromQuery] int itemsPerPage = -1
            )
        {

            var query = new VlanChildrenListQuery { Id = id, Page = page, ItemsPerPage = itemsPerPage, SortBy = sortBy, SortDesc = sortDesc };

            var result = await handler.ExecuteAsync(query).ToApiActionResult(HttpContext);

            return result;
        }
    }
}
