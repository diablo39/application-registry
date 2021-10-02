using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Domain.Entities;
using ApplicationRegistry.Domain.Entities.Applications;
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
    public class NugetPackageApplicationListQuery : ListQueryParameters, IQuery
    {
        public string Name { get; set; }

    }

    public class NugetPackageApplicationListQueryValidator : AbstractValidator<NugetPackageApplicationListQuery>
    {
        public NugetPackageApplicationListQueryValidator()
        {

        }
    }

    public class NugetPackageApplicationListQueryResult : CollectionQueryResultBase<NugetPackageApplicationListQueryResultItem>
    {
        public NugetPackageApplicationListQueryResult(IEnumerable<NugetPackageApplicationListQueryResultItem> items, int count)
            : base(items, count)
        {

        }

    }

    public class NugetPackageApplicationListQueryResultItem
    {
        public Guid ApplicationId { get; set; }

        public string ApplicationName { get; set; }

        public Guid ApplicationVersionId { get; set; }

        public string ApplicationVersion { get; set; }

        public string EnvironmentId { get; set; }

        public string PackageVersion { get; set; }
    }


    public class NugetPackageApplicationListQueryHandler : IQueryHandler<NugetPackageApplicationListQuery, NugetPackageApplicationListQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public NugetPackageApplicationListQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<NugetPackageApplicationListQueryResult>> ExecuteAsync(NugetPackageApplicationListQuery query)
        {
            var dbQuery = _queryModel
                    .ApplicationVersionNugetDependencies
                    .Include(e => e.ApplicationVersion)
                    .ThenInclude(e => e.Application)
                    .Include(e => e.NugetPackage)
                    .Where(e => e.NugetPackage.Name == query.Name && e.ApplicationVersion.IsArchived == false)
                    .Select(MappingDomainToQueryResult());

            var count = await dbQuery.CountAsync();

            

            var items = await dbQuery.ToArrayAsync();

            var result = new NugetPackageApplicationListQueryResult(items, count);

            return OperationResult.Success(result);
        }
        internal static Expression<Func<ApplicationVersionNugetPackageDependency, NugetPackageApplicationListQueryResultItem>> MappingDomainToQueryResult()
        {
            return e => new NugetPackageApplicationListQueryResultItem
            {
                ApplicationName = e.ApplicationVersion.Application.Name,
                ApplicationVersion = e.ApplicationVersion.Version,
                EnvironmentId = e.ApplicationVersion.IdEnvironment,
                PackageVersion = e.NugetPackage.Version,
                ApplicationId = e.ApplicationVersion.IdApplication,
                ApplicationVersionId = e.ApplicationVersion.Id
            };
        }
    }
}
