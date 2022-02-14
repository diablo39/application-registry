using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ApplicationRegistry.Application.Queries
{
    public class ApplicationVersionListQuery : IQuery
    {
        public string IdOrCode { get; set; }

        public bool ShowArchived { get; set; } = false;
    }

    public class ApplicationVersionListQueryValidator : AbstractValidator<ApplicationVersionListQuery>
    {
        public ApplicationVersionListQueryValidator()
        {
            RuleFor(e => e.IdOrCode).NotEmpty().NotNull();
        }
    }

    public class ApplicationVersionListQueryResult : CollectionQueryResultBase<ApplicationVersionListQueryResultItem>
    {
        public ApplicationVersionListQueryResult(IEnumerable<ApplicationVersionListQueryResultItem> items, int count)
            : base(items, count)
        {

        }

    }

    public class ApplicationVersionListQueryResultItem
    {
        public Guid Id { get; set; }

        public Guid IdApplication { get; set; }

        public string IdEnvironment { get; set; }

        public string Version { get; set; }

        public DateTimeOffset CreateDate { get; set; }

        public string IdCommit { get; set; }

        public bool IsArchived { get; set; }

        public string ToolsVersion { get; set; }

        public string ApplicationName { get; internal set; }

        public bool CollectorExecutionSucceeded { get; set; }

        public string CollectorExecutionDuration { get; set; }

        public string CollectorBatchStatuses { get; set; }

        public IEnumerable<SwaggerSpecyfication> SwaggerSpecyfications { get; set; }
        public bool HasSwaggerSpecification { get; internal set; }
    }

    public class SwaggerSpecyfication
    {
        public Guid Id { get; set; }
    }

    public class ApplicationVersionListQueryHandler : IQueryHandler<ApplicationVersionListQuery, ApplicationVersionListQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public ApplicationVersionListQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<ApplicationVersionListQueryResult>> ExecuteAsync(ApplicationVersionListQuery query)
        {
            ApplicationEntity applicationEntity = await _queryModel.Applications.SingleOrDefaultAsync(m => m.Id.ToString() == query.IdOrCode || m.Code == query.IdOrCode);

            if (applicationEntity == null)
            {
                return OperationResult.Success(default(ApplicationVersionListQueryResult));
            }

            var dbQuery = _queryModel
              .ApplicationVersions
              .Include(e => e.Application).ThenInclude(e => e.System)
              .Include(e => e.Environment)
              .Include(e => e.SwaggerSpecifications)
              .Where(e => e.IdApplication == applicationEntity.Id && (query.ShowArchived == true || e.IsArchived == false));

            var count = await dbQuery.CountAsync();
            var results = await dbQuery.ToListAsync();
            var items = results.Select(MappingDomainToQueryResult()).ToArray();

            var result = new ApplicationVersionListQueryResult(items, count);

            return OperationResult.Success(result);
        }
        internal static Func<ApplicationVersionEntity, ApplicationVersionListQueryResultItem> MappingDomainToQueryResult()
        {
            return e => new ApplicationVersionListQueryResultItem
            {
                CreateDate = e.CreateDate,
                Id = e.Id,
                IdApplication = e.IdApplication,
                IdCommit = e.IdCommit,
                IdEnvironment = e.IdEnvironment,
                IsArchived = e.IsArchived,
                ToolsVersion = e.ToolsVersion,
                Version = e.Version,
                ApplicationName = e.Application.Name,
                HasSwaggerSpecification = e.SwaggerSpecifications.Any(),
                CollectorBatchStatuses = e.CollectorBatchStatuses,
                CollectorExecutionDuration = e.CollectorExecutionDuration,
                CollectorExecutionSucceeded = e.CollectorExecutionSucceeded,
            };
        }
    }
}
