using ApplicationRegistry.Application.Attributes;
using ApplicationRegistry.Application.Commands.Redis;
using ApplicationRegistry.Application.Queries;
using ApplicationRegistry.Application.Queries.Redis;
using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Web.Areas.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationRegistry.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(ApiErrorModel), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorModel), StatusCodes.Status422UnprocessableEntity)]
    public class RedisController : ControllerBase
    {
        [HttpGet(Name ="GetRedisList")]
        [ProducesResponseType(typeof(RedisListQueryResult), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType(typeof(ApiErrorModel))]
        public async Task<IActionResult> Get(
            [FromServices] IQueryHandler<RedisListQuery, RedisListQueryResult> handler,
            [FromQuery] string sortBy = null,
            [FromQuery] bool? sortDesc = null,
            [FromQuery] int page = 1,
            [FromQuery] int itemsPerPage = -1)
        {
            RedisListQuery query = new RedisListQuery { Page = page, ItemsPerPage = itemsPerPage, SortBy = sortBy, SortDesc = sortDesc };
            var result = await handler.ExecuteAsync(query).ToApiActionResult(HttpContext);

            return result;
        }

        [HttpGet("deployment-types", Name = "GetDeploymentTypeList")]
        [ProducesResponseType(typeof(RedisListQueryResult), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType(typeof(ApiErrorModel))]
        public async Task<IActionResult> GetDeploymentTypes(
            [FromServices] IQueryHandler<RedisDeploymentTypeQuery, RedisDeploymentTypeQueryResult> handler,
            [FromQuery] string sortBy = null,
            [FromQuery] bool? sortDesc = null,
            [FromQuery] int page = 1,
            [FromQuery] int itemsPerPage = -1)
        {
            var query = new RedisDeploymentTypeQuery { Page = page, ItemsPerPage = itemsPerPage, SortBy = sortBy, SortDesc = sortDesc };
            var result = await handler.ExecuteAsync(query).ToApiActionResult(HttpContext);

            return result;
        }



        [HttpGet("{id}", Name ="GetRedisDetails")]
        [ProducesResponseType(typeof(RedisDetailsQueryResult), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType(typeof(ApiErrorModel))]
        public async Task<IActionResult> Get([FromRoute] Guid id, [FromServices] IQueryHandler<RedisDetailsQuery, RedisDetailsQueryResult> queryHandler)
        {
            RedisDetailsQuery query = new RedisDetailsQuery { Id = id };


            var result = await queryHandler.ExecuteAsync(query).ToApiActionResult(HttpContext);

            return result;
        }

        [HttpPost(Name = "CreateNewRedis")]
        public async Task<IActionResult> Post(
            [FromBody] RedisCreateCommand command, 
            [FromServices] ICommandHandler<RedisCreateCommand, RedisCreateCommandResult> handler)
        {
            var result = await handler.ExecuteAsync(command).ToApiActionResult(HttpContext);

            return result;
        }

        [HttpPut("{id}", Name = "UpdateRedis")]
        public async Task<IActionResult> Put(
            [FromRoute] Guid id,
            [FromBody][SwaggerIgnoreProperty("Id")] RedisUpdateCommand command,
            [FromServices] ICommandHandler<RedisUpdateCommand, RedisUpdateCommandResult> handler)
        {
            command.Id = id;

            var result = await handler.ExecuteAsync(command).ToApiActionResult(HttpContext);

            return result;
        }
    }
}
