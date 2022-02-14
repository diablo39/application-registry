using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Queries
{
    public class ApplicationVersionDetailsQuery : IQuery
    {
        public Guid ApplicationVersionId { get; set; }
    }

    public class ApplicationVersionDetailsQueryValidator : AbstractValidator<ApplicationVersionDetailsQuery>
    {
        public ApplicationVersionDetailsQueryValidator()
        {

        }
    }

    public class ApplicationVersionDetailsQueryResult
    {
        public Guid Id { get; set; }

        public Guid ApplicationId { get; set; }

        public String ApplicationName { get; set; }

        public string EnvironmentId { get; set; }

        public string Version { get; set; }

        public DateTimeOffset CreateDate { get; set; }

        public string IdCommit { get; set; }

        public bool IsArchived { get; set; }

        public string ToolsVersion { get; set; }

        public string FrameworkVersion { get; set; }

        public bool HasSwagger { get; set; }
    }



    public class ApplicationVersionDetailsQueryHandler : IQueryHandler<ApplicationVersionDetailsQuery, ApplicationVersionDetailsQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public ApplicationVersionDetailsQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<ApplicationVersionDetailsQueryResult>> ExecuteAsync(ApplicationVersionDetailsQuery query)
        {
            ApplicationVersionDetailsQueryResult result = null;

            result = await _queryModel.
                ApplicationVersions
                    .Include(e=> e.Environment)
                    .Include(e=> e.Application)
                .Where(e => e.Id == query.ApplicationVersionId).Select(MappingDomainToQueryResult()).SingleOrDefaultAsync();

            var hasSwagger = _queryModel
                .ApplicationVersionSpecifications
                .Where(e => e.IdApplicationVersion == query.ApplicationVersionId)
                .Any();

            result.HasSwagger = hasSwagger;

            return OperationResult.Success(result);
        }

        internal static Expression<Func<ApplicationVersionEntity, ApplicationVersionDetailsQueryResult>> MappingDomainToQueryResult()
        {
            return e => new ApplicationVersionDetailsQueryResult
            {
                CreateDate = e.CreateDate,
                Id = e.Id,
                ApplicationId = e.IdApplication,
                ApplicationName = e.Application.Name,
                IdCommit = e.IdCommit,
                EnvironmentId = e.IdEnvironment,
                IsArchived = e.IsArchived,
                ToolsVersion = e.ToolsVersion,
                Version = e.Version,
                FrameworkVersion = e.FrameworkVersion,
            };
        }
    }
}
