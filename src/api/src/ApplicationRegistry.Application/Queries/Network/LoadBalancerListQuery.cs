using ApplicationRegistry.Application.Services;
using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
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

    public class LoadBalancerListQueryResult : CollectionQueryResultBase<object>
    {
        public LoadBalancerListQueryResult(IEnumerable<object> items, int count)
            : base(items, count)
        {

        }

    }


    public class LoadBalancerListQueryHandler : IQueryHandler<LoadBalancerListQuery, LoadBalancerListQueryResult>
    {
        readonly SotDataProvider _dataProvider;

        public LoadBalancerListQueryHandler(SotDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public async Task<OperationResult<LoadBalancerListQueryResult>> ExecuteAsync(LoadBalancerListQuery query)
        {
            var items = await _dataProvider.GetLoadBalancersAsync();

            var result = new LoadBalancerListQueryResult(items, items.Count);

            return OperationResult.Success(result);
        }

    }


}
