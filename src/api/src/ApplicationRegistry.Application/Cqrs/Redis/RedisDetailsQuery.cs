using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Domain.Entities.Redis;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Queries.Redis
{
    public class RedisDetailsQuery : IQuery
    {
        public Guid Id { get; set; }
    }

    public class RedisDetailsQueryValidator : AbstractValidator<RedisDetailsQuery>
    {
        public RedisDetailsQueryValidator()
        {

        }
    }

    public class RedisDetailsQueryResult
    {
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Requester { get; set; }

        public string RfcId { get; set; }

        public Guid? SentinelRedisId { get; set; }

        public string GroupId { get; set; }

        public string NetworkZone { get; set; }

        public string RedisDeploymentTypeName { get; set; }

        public string EnvironmentId { get; set; }

        public IEnumerable<RedisDetailsEndpoint> Endpoints { get; set; }
    }

    public class RedisDetailsEndpoint
    {
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        public string Host { get; set; }

        public string Port { get; set; }

    }



    public class RedisDetailsQueryHandler : IQueryHandler<RedisDetailsQuery, RedisDetailsQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public RedisDetailsQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<RedisDetailsQueryResult>> ExecuteAsync(RedisDetailsQuery query)
        {
            RedisDetailsQueryResult result = await _queryModel.Redis.Select(MappingDomainToQueryResult()).FirstOrDefaultAsync(e => e.Id == query.Id);

            return OperationResult.Success(result);
        }

        internal static Expression<Func<RedisEntity, RedisDetailsQueryResult>> MappingDomainToQueryResult()
        {
            return e => new RedisDetailsQueryResult
            {
                CreateDate = e.CreateDate,
                Description = e.Description,
                GroupId = e.GroupId,
                Id = e.Id,
                Name = e.Name,
                NetworkZone = e.NetworkZone,
                RedisDeploymentTypeName = e.RedisDeploymentType.Description,
                Requester = e.Requester,
                RfcId = e.RfcId,
                SentinelRedisId = e.SentinelRedisId,
                EnvironmentId = e.Environment.Id,
                Endpoints = e.Endpoints.Select(d => new RedisDetailsEndpoint
                {
                    CreateDate = e.CreateDate,
                    Host = d.Host,
                    Id = d.Id,
                    Port = d.Port
                })
            };
        }
    }
}
