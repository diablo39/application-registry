using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Domain.Entities;
using ApplicationRegistry.Domain.Entities.Redis;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Queries.Redis
{
    public class RedisListQuery : ListQueryParameters, IQuery
    {

    }

    public class RedisListQueryValidator : AbstractValidator<RedisListQuery>
    {
        public RedisListQueryValidator()
        {

        }
    }

    public class RedisListQueryResult : CollectionQueryResultBase<RedisListQueryResultItem>
    {
        public RedisListQueryResult(IEnumerable<RedisListQueryResultItem> items, int count)
            : base(items, count)
        {

        }

    }

    public class RedisListQueryResultItem
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

    }


    public class RedisListQueryHandler : IQueryHandler<RedisListQuery, RedisListQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public RedisListQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<RedisListQueryResult>> ExecuteAsync(RedisListQuery query)
        {
            var dbQuery = _queryModel
              .Redis
              .Select(MappingDomainToQueryResult());

            var count = await dbQuery.CountAsync();
            var items = await dbQuery.ToArrayAsync();

            var result = new RedisListQueryResult(items, count);

            return OperationResult.Success(result);
        }
        internal static Expression<Func<RedisEntity, RedisListQueryResultItem>> MappingDomainToQueryResult()
        {
            return e => new RedisListQueryResultItem
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
                EnvironmentId = e.Environment.Id
            };
        }
    }
}
