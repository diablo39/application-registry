using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ApplicationRegistry.CQRS.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Queries
{
    public class EnvironmentsListQuery : ListQueryParameters, IQuery
    {

    }

    public class EnvironmentsListQueryValidator : AbstractValidator<EnvironmentsListQuery>
    {
        public EnvironmentsListQueryValidator()
        {

        }
    }

    public class EnvironmentsListQueryResult : CollectionQueryResultBase<EnvironmentsListQueryResultItem>
    {
        public EnvironmentsListQueryResult(IEnumerable<EnvironmentsListQueryResultItem> items, int count)
            : base(items, count)
        {

        }

    }

    public class EnvironmentsListQueryResultItem
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }
    }


    public class EnvironmentsListQueryHandler : IQueryHandler<EnvironmentsListQuery, EnvironmentsListQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public EnvironmentsListQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<EnvironmentsListQueryResult>> ExecuteAsync(EnvironmentsListQuery query)
        {
            var dbQuery = _queryModel
              .Environments
              .Select(MappingDomainToQueryResult());

            var count = await dbQuery.CountAsync();

            dbQuery = dbQuery.SortAndFilter(query);

            var items = await dbQuery.ToArrayAsync();

            var result = new EnvironmentsListQueryResult(items, count);

            return OperationResult.Success(result);
        }

        

        internal static Expression<Func<EnvironmentEntity, EnvironmentsListQueryResultItem>> MappingDomainToQueryResult()
        {
            return e => new EnvironmentsListQueryResultItem
            {
                Id = e.Id,
                CreateDate = e.CreateDate,
                Description = e.Description
            };
        }


    }
}
