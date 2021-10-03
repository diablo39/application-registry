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
    public class FirewallRuleListQuery : ListQueryParameters, IQuery
    {

    }

    public class FirewallRuleListQueryValidator : AbstractValidator<FirewallRuleListQuery>
    {
        public FirewallRuleListQueryValidator()
        {

        }
    }

    public class FirewallRuleListQueryResult : CollectionQueryResultBase<object>
    {
        public FirewallRuleListQueryResult(IEnumerable<object> items, int count)
            : base(items, count)
        {

        }

    }


    public class FirewallRuleListQueryHandler : IQueryHandler<FirewallRuleListQuery, FirewallRuleListQueryResult>
    {
        readonly SotDataProvider _dataProvider;

        public FirewallRuleListQueryHandler(SotDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public async Task<OperationResult<FirewallRuleListQueryResult>> ExecuteAsync(FirewallRuleListQuery query)
        {
            var items = await _dataProvider.GetFirewallRulesAsync();

            var result = new FirewallRuleListQueryResult(items, items.Count);

            return OperationResult.Success(result);
        }

    }


}
