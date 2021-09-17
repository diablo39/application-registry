﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationRegistry.Application.Commands.Network;
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
    public class LoadBalancersController : ControllerBase
    {
        // GET: api/<ApplicationsController>
        /*
         {
            "name": "test-lb",
            "port": 443,
            "description": "some description",
            "ip-v4": "192.168.1.1",
            "member-pools": [
                {
                    "name": "main pool",
                    "description": "generated",
                    "port": 80,
                    "balancing": "round-robin",
                    "monitor": "tcp",
                    "members": [
                        {
                            "name": "my-host",
                            "ip-v4": "10.168.152.25"
                        }
                    ]
                }
            ]
        }
        */
        [HttpGet]
        public async Task<IActionResult> Get(
            [FromServices] IQueryHandler<LoadBalancerListQuery, LoadBalancerListQueryResult> handler,
            [FromQuery] string sortBy = null,
            [FromQuery] bool? sortDesc = null,
            [FromQuery] int page = 1,
            [FromQuery] int itemsPerPage = -1)
        {
            var query = new LoadBalancerListQuery() { Page = page, ItemsPerPage = itemsPerPage, SortBy = sortBy, SortDesc = sortDesc };

            var result = await handler.ExecuteAsync(query).ToApiActionResult(HttpContext);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id, [FromServices] IQueryHandler<LoadBalancerDetailsQuery, object> handler)
        {
            var query = new LoadBalancerDetailsQuery() { Id = id };

            var result = await handler.ExecuteAsync(query).ToApiActionResult(HttpContext);

            return result;
        }

        [HttpPost(Name = "CreateLoadBalancer")]
        [ProducesDefaultResponseType(typeof(ApiErrorModel))]
        public async Task<IActionResult> Post(
            [FromBody] LoadBalancerCreateCommand command,
            [FromServices] ICommandHandler<LoadBalancerCreateCommand, LoadBalancerCreateCommandResult> handler)
        {
            var result = await handler.ExecuteAsync(command).ToApiActionResult(HttpContext);

            return result;
        }
    }
}
