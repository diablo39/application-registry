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
    public class ApplicationEndpointListQuery : ListQueryParameters, IQuery
    {
        public Guid IdVersion { get; set; }
    }

    public class ApplicationEndpointListQueryValidator : AbstractValidator<ApplicationEndpointListQuery>
    {
        public ApplicationEndpointListQueryValidator()
        {
          
        }
    }

    public class ApplicationEndpointListQueryResult : CollectionQueryResultBase<ApplicationEndpointListQueryResultItem>
    {
        public ApplicationEndpointListQueryResult(IEnumerable<ApplicationEndpointListQueryResultItem> items, int count)
            : base(items, count)
        {

        }

    }

    public class ApplicationEndpointListQueryResultItem
    {
        public string Path { get; set; }

        public string OperationId { get; set; }

        public string HttpMethod { get; set; }
    }


    public class ApplicationEndpointListQueryHandler : IQueryHandler<ApplicationEndpointListQuery, ApplicationEndpointListQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public ApplicationEndpointListQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<ApplicationEndpointListQueryResult>> ExecuteAsync(ApplicationEndpointListQuery query)
        {
            var dbQuery = _queryModel
              .SwaggerSpecificationOperations
              .Include(e=> e.Specification)
              .Where(e=> e.Specification.IdApplicationVersion == query.IdVersion)
              .Select(MappingDomainToQueryResult());

            var count = await dbQuery.CountAsync();
            var items = await dbQuery.ToArrayAsync();

            var result = new ApplicationEndpointListQueryResult(items, count);

            return OperationResult.Success(result);
        }
        internal static Expression<Func<SwaggerSpecificationOperationEntity, ApplicationEndpointListQueryResultItem>> MappingDomainToQueryResult()
        {
            return e => new ApplicationEndpointListQueryResultItem
            {
                HttpMethod = e.HttpMethod,
                OperationId = e.OperationId,
                Path = e.Path,
            };
        }
    }
}
