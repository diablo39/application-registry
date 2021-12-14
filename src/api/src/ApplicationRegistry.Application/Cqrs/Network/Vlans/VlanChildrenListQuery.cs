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

namespace ApplicationRegistry.Application.Cqrs.Network.Vlans
{
    public class VlanChildrenListQuery : ListQueryParameters, IQuery
    {
        public Guid id { get; set; }
    }

    public class VlanChildrenListQueryValidator : AbstractValidator<VlanChildrenListQuery>
    {
        public VlanChildrenListQueryValidator()
        {

        }
    }

    public class VlanChildrenListQueryResult : CollectionQueryResultBase<VlanChildrenListQueryResultItem>
    {
        public VlanChildrenListQueryResult(IEnumerable<VlanChildrenListQueryResultItem> items, int count)
            : base(items, count)
        {

        }

    }

    public class VlanChildrenListQueryResultItem
    {

    }


    public class VlanChildrenListQueryHandler : IQueryHandler<VlanChildrenListQuery, VlanChildrenListQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public VlanChildrenListQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<VlanChildrenListQueryResult>> ExecuteAsync(VlanChildrenListQuery query)
        {
            var item = await _queryModel.Vlans.FirstOrDefaultAsync(e=> e.Id == query.id);



            if (item == null) return OperationResult.Success<VlanChildrenListQueryResult>(null);

            var dbQuery = _queryModel
              .Vlans
              .Select(MappingDomainToQueryResult());

            var count = await dbQuery.CountAsync();
            var items = await dbQuery.ToArrayAsync();

            var result = new VlanChildrenListQueryResult(items, count);

            return OperationResult.Success(result);
        }
        internal static Expression<Func<VlanEntity, VlanChildrenListQueryResultItem>> MappingDomainToQueryResult()
        {
            return e => new VlanChildrenListQueryResultItem
            {

            };
        }
    }
}
