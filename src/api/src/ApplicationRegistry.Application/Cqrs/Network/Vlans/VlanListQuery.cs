using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Domain.Entities;
using ApplicationRegistry.Domain.Entities.Network;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Queries
{
    public class VlanListQuery : ListQueryParameters, IQuery
    {

    }

    public class VlanListQueryValidator : AbstractValidator<VlanListQuery>
    {
        public VlanListQueryValidator()
        {

        }
    }

    public class VlanListQueryResult : CollectionQueryResultBase<VlanListQueryResultItem>
    {
        public VlanListQueryResult(IEnumerable<VlanListQueryResultItem> items, int count)
            : base(items, count)
        {

        }

    }

    public class VlanListQueryResultItem
    {

    }


    public class VlanListQueryHandler : IQueryHandler<VlanListQuery, VlanListQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public VlanListQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<VlanListQueryResult>> ExecuteAsync(VlanListQuery query)
        {
            var dbQuery = _queryModel
              .Vlans
              .Select(MappingDomainToQueryResult());

            var count = await dbQuery.CountAsync();
            var items = await dbQuery.ToArrayAsync();

            var result = new VlanListQueryResult(items, count);

            return OperationResult.Success(result);
        }
        internal static Expression<Func<VlanEntity, VlanListQueryResultItem>> MappingDomainToQueryResult()
        {
            return e => new VlanListQueryResultItem
            {

            };
        }
    }
}
