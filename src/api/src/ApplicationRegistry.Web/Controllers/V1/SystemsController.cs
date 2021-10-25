using ApplicationRegistry.Application.Attributes;
using ApplicationRegistry.Application.Cqrs.Systems;
using ApplicationRegistry.CQRS.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace ApplicationRegistry.Web.Areas.Api.Controllers
{
    [Route("api/systems")]
    [ApiController]
    public class SystemsController : ControllerBase
    {
        // GET: api/<SystemsController>
        [HttpGet(Name = "GetSystemList")]
        public async Task<IActionResult> Get([FromServices] IQueryHandler<SystemsListQuery, SystemsListQueryResult> handler)
        {
            var query = new SystemsListQuery();
            var result = await handler.ExecuteAsync(query).ToApiActionResult(HttpContext);
            return result;
        }

        //GET api/<SystemsController>/5
        [HttpGet("{id}", Name = "GetSystemDetails")]
        public async Task<IActionResult> Get(Guid id, [FromServices] IQueryHandler<SystemDetailsQuery, SystemDetailsQueryResult> handler)
        {
            var query = new SystemDetailsQuery { Id = id };
            var result = await handler.ExecuteAsync(query).ToApiActionResult(HttpContext);
            return result;
        }

        // POST api/<SystemsController>
        [HttpPost(Name ="CreateSystem")]
        public async Task<IActionResult> Post([FromBody] SystemCreateCommand command, [FromServices] ICommandHandler<SystemCreateCommand, SystemCreateCommandResult> handler)
        {
            var result = await handler.ExecuteAsync(command).ToApiActionResult(HttpContext);

            return result;
        }

        [HttpPut("{id}", Name = "UpdateSystem")]
        public async Task<IActionResult> Put(Guid? id, [FromBody][SwaggerIgnoreProperty("Id")] SystemUpdateCommand command, [FromServices] ICommandHandler<SystemUpdateCommand, SystemUpdateCommandResult> handler)
        {
            command.Id = id;

            var result = await handler.ExecuteAsync(command).ToApiActionResult(HttpContext);

            return result;
        }

        // // DELETE api/<SystemsController>/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}
