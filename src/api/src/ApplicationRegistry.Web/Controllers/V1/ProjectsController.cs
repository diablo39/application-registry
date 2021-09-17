using ApplicationRegistry.Application.Commands;
using ApplicationRegistry.Application.Queries;
using ApplicationRegistry.CQRS.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApplicationRegistry.Web.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        // GET: api/<ProjectsController>
        [HttpGet]
        public async Task<IActionResult> Get([FromServices] IQueryHandler<ProjectsListQuery, ProjectsListQueryResult> handler)
        {
            var query = new ProjectsListQuery();
            var result = await handler.ExecuteAsync(query).ToApiActionResult(HttpContext);
            return result;
        }

        //GET api/<ProjectsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, [FromServices] IQueryHandler<ProjectDetailsQuery, ProjectDetailsQueryResult> handler)
        {
            var query = new ProjectDetailsQuery { Id = id };
            var result = await handler.ExecuteAsync(query).ToApiActionResult(HttpContext);
            return result;
        }

        // POST api/<ProjectsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProjectCreateCommand command, [FromServices] ICommandHandler<ProjectCreateCommand, ProjectCreateCommandResult> handler)
        {
            var result = await handler.ExecuteAsync(command).ToApiActionResult(HttpContext);

            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid? id, [FromBody] ProjectUpdateCommand command, [FromServices] ICommandHandler<ProjectUpdateCommand, ProjectUpdateCommandResult> handler)
        {
            command.Id = id;

            var result = await handler.ExecuteAsync(command).ToApiActionResult(HttpContext);

            return result;
        }

        // // DELETE api/<ProjectsController>/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}
