﻿using ApplicationRegistry.Application.Commands;
using ApplicationRegistry.Application.Queries;
using ApplicationRegistry.Application.Queries.ApplicationsList;
using ApplicationRegistry.Web.Areas.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationRegistry.CQRS.Abstraction;
using System;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApplicationRegistry.Web.Areas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(ApiErrorModel), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorModel), StatusCodes.Status422UnprocessableEntity)]
    public class ApplicationsController : ControllerBase
    {
        // GET: api/<ApplicationsController>
        [HttpGet(Name = "GetApplicationList")]
        [ProducesResponseType(typeof(ApplicationsListQueryResult), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType(typeof(ApiErrorModel))]
        public async Task<IActionResult> Get(
            [FromServices] IQueryHandler<ApplicationsListQuery, ApplicationsListQueryResult> handler,
            [FromQuery] string sortBy = null,
            [FromQuery] bool? sortDesc = null,
            [FromQuery] int page = 1,
            [FromQuery] int itemsPerPage = -1)
        {
            ApplicationsListQuery query = new ApplicationsListQuery() { Page = page, ItemsPerPage = itemsPerPage, SortBy = sortBy, SortDesc = sortDesc };
            var result = await handler.ExecuteAsync(query).ToApiActionResult(HttpContext);

            return result;
        }

        // GET api/<ApplicationsController>/5

        [HttpGet("{idOrCode}", Name = "GetApplicationDetails")]
        [ProducesResponseType(typeof(ApplicationDetailsQueryResult), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType(typeof(ApiErrorModel))]
        public async Task<IActionResult> Get(
            [FromRoute] string idOrCode, 
            [FromServices] IQueryHandler<ApplicationDetailsQuery, ApplicationDetailsQueryResult> queryHandler)
        {
            ApplicationDetailsQuery query = new ApplicationDetailsQuery { IdOrCode = idOrCode };

            var result = await queryHandler.ExecuteAsync(query).ToApiActionResult(HttpContext);

            return result;
        }

        // GET: api/<ApplicationsController>
        [HttpGet("{idOrCode}/versions", Name = "GetApplicationVersions")]
        [ProducesResponseType(typeof(ApplicationVersionListQueryResult), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType(typeof(ApiErrorModel))]
        public async Task<IActionResult> Get(
            [FromRoute] string idOrCode, 
            [FromServices] IQueryHandler<ApplicationVersionListQuery, ApplicationVersionListQueryResult> handler)
        {
            var query = new ApplicationVersionListQuery
            {
                IdOrCode = idOrCode
            };

            var result = await handler.ExecuteAsync(query).ToApiActionResult(HttpContext);

            return result;
        }



        // POST api/<ApplicationsController>
        [HttpPost(Name = "CreateApplication")]
        [ProducesDefaultResponseType(typeof(ApiErrorModel))]
        public async Task<IActionResult> Post(
            [FromBody] ApplicationCreateCommand command, 
            [FromServices] ICommandHandler<ApplicationCreateCommand, ApplicationCreateCommandResult> handler)
        {
            var result = await handler.ExecuteAsync(command).ToApiActionResult(HttpContext);

            return result;
        }

        // PUT api/<ApplicationsController>/5
        [HttpPut("{idOrCode}", Name = "UpdateApplication")]
        [ProducesDefaultResponseType(typeof(ApiErrorModel))]
        public async Task<IActionResult> Put(
            [FromRoute] Guid idOrCode, 
            [FromBody] ApplicationUpdateCommand command, 
            [FromServices] ICommandHandler<ApplicationUpdateCommand, ApplicationUpdateCommandResult> handler)
        {
            command.Id = idOrCode;

            var result = await handler.ExecuteAsync(command).ToApiActionResult(HttpContext);

            return result;
        }

        //// DELETE api/<ApplicationsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
