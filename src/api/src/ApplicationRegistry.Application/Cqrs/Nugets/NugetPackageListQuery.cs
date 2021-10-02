using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Domain.Entities;
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
    public class NugetPackageListQuery : ListQueryParameters, IQuery
    {

    }

    public class NugetPackageListQueryValidator : AbstractValidator<NugetPackageListQuery>
    {
        public NugetPackageListQueryValidator()
        {

        }
    }

    public class NugetPackageListQueryResult : CollectionQueryResultBase<NugetPackageListQueryResultItem>
    {
        public NugetPackageListQueryResult(IEnumerable<NugetPackageListQueryResultItem> items, int count)
            : base(items, count)
        {

        }

    }

    public class NugetPackageListQueryResultItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }


    public class NugetPackageListQueryHandler : IQueryHandler<NugetPackageListQuery, NugetPackageListQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public NugetPackageListQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<NugetPackageListQueryResult>> ExecuteAsync(NugetPackageListQuery query)
        {
            var dbQuery = _queryModel
              .NugetPackages
              .Select(MappingDomainToQueryResult())
              .Distinct();

            var count = await dbQuery.CountAsync();
            var items = await dbQuery.ToArrayAsync();

            var result = new NugetPackageListQueryResult(items, count);

            return OperationResult.Success(result);
        }

        internal static Expression<Func<NugetPackageEntity, NugetPackageListQueryResultItem>> MappingDomainToQueryResult()
        {
            return e => new NugetPackageListQueryResultItem
            {
                Id = e.Name,
                Name = e.Name,
            };
        }
    }


}
