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
    public class NugetPackageVersionListQuery : ListQueryParameters, IQuery
    {
        public string Id { get; set; }
    }

    public class NugetPackageVersionListQueryValidator : AbstractValidator<NugetPackageVersionListQuery>
    {
        public NugetPackageVersionListQueryValidator()
        {

        }
    }

    public class NugetPackageVersionListQueryResult : CollectionQueryResultBase<NugetPackageVersionListQueryResultItem>
    {
        public NugetPackageVersionListQueryResult(IEnumerable<NugetPackageVersionListQueryResultItem> items, int count)
            : base(items, count)
        {

        }

    }

    public class NugetPackageVersionListQueryResultItem
    {
        public string Name { get; set; }

        public string Version { get; set; }

        public DateTime CreateDate { get; set; }

        public int ApplicationVersionsCount { get; set; }
    }


    public class NugetPackageVersionListQueryHandler : IQueryHandler<NugetPackageVersionListQuery, NugetPackageVersionListQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public NugetPackageVersionListQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<NugetPackageVersionListQueryResult>> ExecuteAsync(NugetPackageVersionListQuery query)
        {
            var dbQuery = _queryModel
              .NugetPackages
              //.Include(e=> e.ApplicationVersions)
              .Where(e => e.Name == query.Id)
              .Select(MappingDomainToQueryResult());

            var count = await dbQuery.CountAsync();
            var items = await dbQuery.ToArrayAsync();

            var result = new NugetPackageVersionListQueryResult(items, count);

            return OperationResult.Success(result);
        }
        internal static Expression<Func<NugetPackageEntity, NugetPackageVersionListQueryResultItem>> MappingDomainToQueryResult()
        {
            return e => new NugetPackageVersionListQueryResultItem
            {
                CreateDate = e.CreateDate,
                Name = e.Name,
                Version = e.Version,
                ApplicationVersionsCount = e.ApplicationVersions.Where(a => a.ApplicationVersion.IsArchived == false).Count()
            };
        }
    }
}
