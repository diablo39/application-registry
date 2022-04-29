using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationRegistry.Application.Cqrs.ApplicationVersions
{
    public class ApplicationVersionsSpecificationsListQuery : ListQueryParameters, IQuery
    {
        public Guid? IdVersion { get; set; }
    }

    public class ApplicationVersionsSpecificationsListQueryValidator : AbstractValidator<ApplicationVersionsSpecificationsListQuery>
    {
        public ApplicationVersionsSpecificationsListQueryValidator()
        {
        }
    }

    public class ApplicationVersionsSpecificationsListQueryResult : CollectionQueryResultBase<ApplicationVersionsSpecificationsListQueryResultItem>
    {
        public ApplicationVersionsSpecificationsListQueryResult(IEnumerable<ApplicationVersionsSpecificationsListQueryResultItem> items, int count)
            : base(items, count)
        {
        }
    }

    public class ApplicationVersionsSpecificationsListQueryResultItem
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public DateTimeOffset CreateDate { get; set; }
    }

    public class ApplicationVersionsSpecificationsListQueryHandler : IQueryHandler<ApplicationVersionsSpecificationsListQuery, ApplicationVersionsSpecificationsListQueryResult>
    {
        private readonly IQueryDataModel _queryModel;

        public ApplicationVersionsSpecificationsListQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<ApplicationVersionsSpecificationsListQueryResult>> ExecuteAsync(ApplicationVersionsSpecificationsListQuery query)
        {
            var dbQuery = _queryModel
              .ApplicationVersionSwaggerSpecifications
              .Where(e => e.IdApplicationVersion == query.IdVersion)
              .Select(MappingDomainToQueryResult());

            var count = await dbQuery.CountAsync();
            var items = await dbQuery.ToArrayAsync();

            var result = new ApplicationVersionsSpecificationsListQueryResult(items, count);

            return OperationResult.Success(result);
        }

        internal static Expression<Func<ApplicationVersionSwaggerSpecificationEntity, ApplicationVersionsSpecificationsListQueryResultItem>> MappingDomainToQueryResult()
        {
            return e => new ApplicationVersionsSpecificationsListQueryResultItem
            {
                Id = e.Id,
                Name = e.Code,
                Type = "OpenApi",
                CreateDate = e.CreateDate,
            };
        }
    }
}