using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Domain.Entities;
using ApplicationRegistry.Domain.Entities.Applications;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Queries
{
    public class NugetPackageVersionDetailsQuery : IQuery
    {
        public string Name { get; set; }

        public string Version { get; set; }
    }

    public class NugetPackageVersionDetailsQueryValidator : AbstractValidator<NugetPackageVersionDetailsQuery>
    {
        public NugetPackageVersionDetailsQueryValidator()
        {

        }
    }

    public class NugetPackageVersionDetailsQueryResult
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Version { get; set; }

        public List<NugetPackageVersionDetailsQueryResultApplication> Applications { get; set; }
    }

    public class NugetPackageVersionDetailsQueryResultApplication
    {
        public Guid ApplicationVersionId { get; set; }

        public string ApplicationVersion { get; set; }

        public Guid ApplicationId { get; set; }


        public string ApplicationName { get; set; }

        public string EnvironmentId { get; set; }
    }

    public class NugetPackageVersionDetailsQueryHandler : IQueryHandler<NugetPackageVersionDetailsQuery, NugetPackageVersionDetailsQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public NugetPackageVersionDetailsQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<NugetPackageVersionDetailsQueryResult>> ExecuteAsync(NugetPackageVersionDetailsQuery query)
        {
            NugetPackageVersionDetailsQueryResult result = default;

            var packageVersion = await _queryModel.NugetPackages.FirstOrDefaultAsync(e=> e.Name == query.Name && e.Version == query.Version);

            if(packageVersion == default)
                return OperationResult.Success(result);

            var applicationVersions = _queryModel
                .ApplicationVersionNugetDependencies
                .Include(e => e.ApplicationVersion)
                .ThenInclude(e=> e.Application)
                .Where(e => e.IdNugetPackage == packageVersion.Id && e.ApplicationVersion.IsArchived == false)
                .Select(MappingDomainToQueryResult());

            result = new NugetPackageVersionDetailsQueryResult
            {
                Id = packageVersion.Id,
                Name = packageVersion.Name,
                Version = packageVersion.Version,
                Applications = applicationVersions.ToList()
            };


            return OperationResult.Success(result);
        }

        internal static Expression<Func<ApplicationVersionNugetPackageDependency, NugetPackageVersionDetailsQueryResultApplication>> MappingDomainToQueryResult()
        {
            return e => new NugetPackageVersionDetailsQueryResultApplication
            {
                ApplicationId = e.ApplicationVersion.IdApplication,
                ApplicationName = e.ApplicationVersion.Application.Name,
                ApplicationVersionId = e.ApplicationVersion.Id,
                ApplicationVersion = e.ApplicationVersion.Version,
                EnvironmentId = e.ApplicationVersion.IdEnvironment
            };
        }
    }


}
