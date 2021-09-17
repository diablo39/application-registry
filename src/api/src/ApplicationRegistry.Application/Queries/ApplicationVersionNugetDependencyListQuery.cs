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
    public class ApplicationVersionNugetDependencyListQuery : ListQueryParameters, IQuery
    {
        public Guid ApplicationVersionId { get; set; }
    }

    public class ApplicationVersionNugetDependencyListQueryValidator : AbstractValidator<ApplicationVersionNugetDependencyListQuery>
    {
        public ApplicationVersionNugetDependencyListQueryValidator()
        {

        }
    }

    public class ApplicationVersionNugetDependencyListQueryResult : CollectionQueryResultBase<ApplicationVersionNugetDependencyListQueryResultItem>
    {
        public ApplicationVersionNugetDependencyListQueryResult(IEnumerable<ApplicationVersionNugetDependencyListQueryResultItem> items, int count)
            : base(items, count)
        {

        }

    }

    public class ApplicationVersionNugetDependencyListQueryResultItem
    {
        public string Name { get; set; }

        public string Version { get; set; }
    }


    public class ApplicationVersionNugetDependencyListQueryHandler : IQueryHandler<ApplicationVersionNugetDependencyListQuery, ApplicationVersionNugetDependencyListQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public ApplicationVersionNugetDependencyListQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<ApplicationVersionNugetDependencyListQueryResult>> ExecuteAsync(ApplicationVersionNugetDependencyListQuery query)
        {
            var dbQuery = _queryModel
              .NugetPackages
              .Include(e=> e.ApplicationVersions)
              .Where(e=> e.ApplicationVersions.Any(a=> a.IdApplicationVersion == query.ApplicationVersionId))
              .Select(MappingDomainToQueryResult());

            var count = await dbQuery.CountAsync();
            var items = await dbQuery.ToArrayAsync();

            var result = new ApplicationVersionNugetDependencyListQueryResult(items, count);

            return OperationResult.Success(result);
        }
        internal static Expression<Func<NugetPackageEntity, ApplicationVersionNugetDependencyListQueryResultItem>> MappingDomainToQueryResult()
        {
            return e => new ApplicationVersionNugetDependencyListQueryResultItem
            {
                Name = e.Name,
                Version = e.Version
            };
        }
    }
}
