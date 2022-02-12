using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Cqrs.Systems
{
    public class SystemsListQuery : IQuery
    {

    }

    public class SystemsListQueryValidator : AbstractValidator<SystemsListQuery>
    {
        public SystemsListQueryValidator()
        {

        }
    }

    public class SystemsListQueryResult : CollectionQueryResultBase<SystemsListQueryResultItem>
    {
        public SystemsListQueryResult(IEnumerable<SystemsListQueryResultItem> items, int count)
            : base(items, count)
        {

        }

    }

    public class SystemsListQueryResultItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreateDate { get; set; }
    }


    public class SystemsListQueryHandler : IQueryHandler<SystemsListQuery, SystemsListQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public SystemsListQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<SystemsListQueryResult>> ExecuteAsync(SystemsListQuery query)
        {
            var dbQuery = _queryModel
              .Systems
              .Select(MappingDomainToQueryResult());

            var count = await dbQuery.CountAsync();
            var items = await dbQuery.ToArrayAsync();

            var result = new SystemsListQueryResult(items, count);

            return OperationResult.Success(result);
        }
        internal static Expression<Func<SystemEntity, SystemsListQueryResultItem>> MappingDomainToQueryResult()
        {
            return e => new SystemsListQueryResultItem
            {
                CreateDate = e.CreateDate,
                Description = e.Description,
                Id = e.Id,
                Name = e.Name

            };
        }
    }
}
