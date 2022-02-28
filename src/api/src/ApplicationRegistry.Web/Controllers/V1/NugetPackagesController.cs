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
    public class NugetPackagesController : ControllerBase
    {
        // GET: api/<ApplicationsController>
        [HttpGet]
        public async Task<IActionResult> Get(
            [FromServices] IQueryHandler<NugetPackageListQuery, NugetPackageListQueryResult> handler,
            [FromQuery] string sortBy = null,
            [FromQuery] bool? sortDesc = null,
            [FromQuery] int page = 1,
            [FromQuery] int itemsPerPage = -1)
        {
            NugetPackageListQuery query = new NugetPackageListQuery() { Page = page, ItemsPerPage = itemsPerPage, SortBy = sortBy, SortDesc = sortDesc };

            var result = await handler.ExecuteAsync(query).ToApiActionResultAsync(HttpContext);

            return result;
        }

        //[HttpGet("{id}")]
        //[ProducesResponseType(typeof(NugetPackageDetailsQueryResult), StatusCodes.Status200OK)]
        //public async Task<IActionResult> Get([FromRoute] long? id, [FromServices] IQueryHandler<NugetPackageDetailsQuery, NugetPackageDetailsQueryResult> queryHandler)
        //{
        //    NugetPackageDetailsQuery query = new NugetPackageDetailsQuery { Id = id };


        //    var result = await queryHandler.ExecuteAsync(query).ToApiActionResult(HttpContext);

        //    return result;
        //}

        [HttpGet("{id}/versions")]
        [ProducesResponseType(typeof(NugetPackageDetailsQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVersions([FromRoute] string id, [FromServices] IQueryHandler<NugetPackageVersionListQuery, NugetPackageVersionListQueryResult> queryHandler)
        {
            var query = new NugetPackageVersionListQuery { Id = id };


            var result = await queryHandler.ExecuteAsync(query).ToApiActionResultAsync(HttpContext);

            return result;
        }

        [HttpGet("{id}/applications")]
        [ProducesResponseType(typeof(NugetPackageApplicationListQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetApplications([FromRoute] string id, [FromServices] IQueryHandler<NugetPackageApplicationListQuery, NugetPackageApplicationListQueryResult> queryHandler)
        {
            var query = new NugetPackageApplicationListQuery { Name = id };

            var result = await queryHandler.ExecuteAsync(query).ToApiActionResultAsync(HttpContext);

            return result;
        }

        [HttpGet("{packageName}/{packageVersion}")]
        [ProducesResponseType(typeof(NugetPackageDetailsQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVersionDetailss([FromRoute] string packageName, [FromRoute] string packageVersion, [FromServices] IQueryHandler<NugetPackageVersionDetailsQuery, NugetPackageVersionDetailsQueryResult> queryHandler)
        {
            NugetPackageVersionDetailsQuery query = new NugetPackageVersionDetailsQuery
            {
                Name = packageName,
                Version = packageVersion
            };

            var result = await queryHandler.ExecuteAsync(query).ToApiActionResultAsync(HttpContext);

            return result;
        }
    }
}
