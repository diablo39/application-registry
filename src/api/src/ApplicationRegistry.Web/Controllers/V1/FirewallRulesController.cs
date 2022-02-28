using ApplicationRegistry.Application.Queries;
using ApplicationRegistry.CQRS.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationRegistry.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirewallRulesController : ControllerBase
    {
        // GET: api/<ApplicationsController>
        [HttpGet]
        public async Task<IActionResult> Get(
            [FromServices] IQueryHandler<FirewallRuleListQuery, FirewallRuleListQueryResult> handler,
            [FromQuery] string sortBy = null,
            [FromQuery] bool? sortDesc = null,
            [FromQuery] int page = 1,
            [FromQuery] int itemsPerPage = -1)
        {
            FirewallRuleListQuery query = new FirewallRuleListQuery() { Page = page, ItemsPerPage = itemsPerPage, SortBy = sortBy, SortDesc = sortDesc };

            var result = await handler.ExecuteAsync(query).ToApiActionResultAsync(HttpContext);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id, [FromServices] IQueryHandler<FirewallRuleDetailsQuery, object> handler)
        {
            var query = new FirewallRuleDetailsQuery() { Id = id };

            var result = await handler.ExecuteAsync(query).ToApiActionResultAsync(HttpContext);

            return result;
        }
    }
}
