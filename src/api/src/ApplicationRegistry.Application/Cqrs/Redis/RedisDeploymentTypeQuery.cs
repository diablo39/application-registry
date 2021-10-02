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
    public class RedisDeploymentTypeQuery : ListQueryParameters, IQuery
    {

    }

    public class RedisDeploymentTypeQueryValidator : AbstractValidator<RedisDeploymentTypeQuery>
    {
        public RedisDeploymentTypeQueryValidator()
        {

        }
    }

    public class RedisDeploymentTypeQueryResult : CollectionQueryResultBase<RedisDeploymentTypeQueryResultItem>
    {
        public RedisDeploymentTypeQueryResult(IEnumerable<RedisDeploymentTypeQueryResultItem> items, int count)
            : base(items, count)
        {

        }

    }

    public class RedisDeploymentTypeQueryResultItem
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }
    }


    public class RedisDeploymentTypeQueryHandler : IQueryHandler<RedisDeploymentTypeQuery, RedisDeploymentTypeQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public RedisDeploymentTypeQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<RedisDeploymentTypeQueryResult>> ExecuteAsync(RedisDeploymentTypeQuery query)
        {
            var dbQuery = _queryModel
              .RedisDeploymentTypes
              .Select(MappingDomainToQueryResult());

            var count = await dbQuery.CountAsync();
            var items = await dbQuery.ToArrayAsync();

            var result = new RedisDeploymentTypeQueryResult(items, count);

            return OperationResult.Success(result);
        }
        internal static Expression<Func<RedisDeploymentTypeEntity, RedisDeploymentTypeQueryResultItem>> MappingDomainToQueryResult()
        {
            return e => new RedisDeploymentTypeQueryResultItem
            {
                CreateDate = e.CreateDate,
                Description = e.Description,
                Id = e.Id
            };
        }
    }
}
