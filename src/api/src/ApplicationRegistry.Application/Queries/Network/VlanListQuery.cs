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
    public class VlansListQuery : ListQueryParameters, IQuery
    {

    }

    public class VlansListQueryValidator : AbstractValidator<VlansListQuery>
    {
        public VlansListQueryValidator()
        {

        }
    }

    public class VlansListQueryResult : CollectionQueryResultBase<VlanListItemModel>
    {
        public VlansListQueryResult(IEnumerable<VlanListItemModel> items, int count)
            : base(items, count)
        {

        }

    }


    public class VlansListQueryHandler : IQueryHandler<VlansListQuery, VlansListQueryResult>
    {
        readonly SotDataProvider _dataProvider;

        public VlansListQueryHandler(SotDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public async Task<OperationResult<VlansListQueryResult>> ExecuteAsync(VlansListQuery query)
        {
            var items = await _dataProvider.GetVlansAsync();

            var result = new VlansListQueryResult(items, items.Count);

            return OperationResult.Success(result);
        }

    }


}
