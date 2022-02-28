using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationRegistry.Application.Cqrs.ApplicationVersions
{
    public class ApplicationVersionEndpointListQuery : ListQueryParameters, IQuery
    {
        public Guid IdVersion { get; set; }
    }

    public class ApplicationVersionEndpointListQueryValidator : AbstractValidator<ApplicationVersionEndpointListQuery>
    {
        public ApplicationVersionEndpointListQueryValidator()
        {

        }
    }

    public class ApplicationVersionEndpointListQueryResult : CollectionQueryResultBase<ApplicationVersionEndpointListQueryResultItem>
    {
        public ApplicationVersionEndpointListQueryResult(IEnumerable<ApplicationVersionEndpointListQueryResultItem> items, int count)
            : base(items, count)
        {

        }

    }

    public class ApplicationVersionEndpointListQueryResultItem
    {
        public string Path { get; set; }

        public string OperationId { get; set; }

        public string HttpMethod { get; set; }
    }


    public class ApplicationVersionEndpointListQueryHandler : IQueryHandler<ApplicationVersionEndpointListQuery, ApplicationVersionEndpointListQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public ApplicationVersionEndpointListQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<ApplicationVersionEndpointListQueryResult>> ExecuteAsync(ApplicationVersionEndpointListQuery query)
        {
            var dbQuery = _queryModel
              .SwaggerSpecificationOperations
              .Include(e => e.Specification)
              .Where(e => e.Specification.IdApplicationVersion == query.IdVersion)
              .Select(MappingDomainToQueryResult());

            var count = await dbQuery.CountAsync();
            var items = await dbQuery.ToArrayAsync();

            var result = new ApplicationVersionEndpointListQueryResult(items, count);

            return OperationResult.Success(result);
        }
        internal static Expression<Func<SwaggerSpecificationOperationEntity, ApplicationVersionEndpointListQueryResultItem>> MappingDomainToQueryResult()
        {
            return e => new ApplicationVersionEndpointListQueryResultItem
            {
                HttpMethod = e.HttpMethod,
                OperationId = e.OperationId,
                Path = e.Path,
            };
        }
    }
}
