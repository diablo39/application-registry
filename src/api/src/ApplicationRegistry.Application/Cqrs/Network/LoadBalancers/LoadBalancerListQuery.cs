using ApplicationRegistry.Application.Services;
using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Domain.Entities.Network;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Queries
{
    public class LoadBalancerListQuery : ListQueryParameters, IQuery
    {

    }

    public class LoadBalancerListQueryValidator : AbstractValidator<LoadBalancerListQuery>
    {
        public LoadBalancerListQueryValidator()
        {

        }
    }

    public class LoadBalancerListQueryResult : CollectionQueryResultBase<LoadBalancerListQueryResultItem>
    {
        public LoadBalancerListQueryResult(IEnumerable<LoadBalancerListQueryResultItem> items, int count)
            : base(items, count)
        {

        }

    }

    public class LoadBalancerListQueryResultItem
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreateDate { get; set; }

        public string Name { get; set; }

        public string Ip { get; set; }

        public string Port { get; set; }

        public string Description { get; set; }

        public string Fqdn { get; set; }
    }

    public class LoadBalancerListQueryHandler : IQueryHandler<LoadBalancerListQuery, LoadBalancerListQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public LoadBalancerListQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<LoadBalancerListQueryResult>> ExecuteAsync(LoadBalancerListQuery query)
        {
            var dbQuery = _queryModel
                              .LoadBalancers
                              .Select(MappingDomainToQueryResult());

            var count = await dbQuery.CountAsync();

            dbQuery = dbQuery.SortAndPage(query);

            var items = await dbQuery.ToArrayAsync();

            var result = new LoadBalancerListQueryResult(items, count);

            return OperationResult.Success(result);
        }

        internal static Expression<Func<LoadBalancerEntity, LoadBalancerListQueryResultItem>> MappingDomainToQueryResult()
        {
            return e => new LoadBalancerListQueryResultItem
            {
                CreateDate = e.CreateDate,
                Description = e.Description,
                Fqdn = e.Fqdn,
                Id = e.Id,
                Ip = e.Ip,
                Name = e.Name,
                Port = e.Port
            };
        }
    }
}
