using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationRegistry.Application.Cqrs.ApplicationVersions
{
    public class ApplicationVersionSwaggerListQuery : ListQueryParameters, IQuery
    {
        public Guid ApplicationVersionId { get; set; }
    }

    public class ApplicationVersionSwaggerListQueryValidator : AbstractValidator<ApplicationVersionSwaggerListQuery>
    {
        public ApplicationVersionSwaggerListQueryValidator()
        {
            RuleFor(e => e.ApplicationVersionId).NotEqual(default(Guid));
        }
    }

    public class ApplicationVersionSwaggerListQueryResult : CollectionQueryResultBase<ApplicationVersionSwaggerListQueryResultItem>
    {
        public ApplicationVersionSwaggerListQueryResult(IEnumerable<ApplicationVersionSwaggerListQueryResultItem> items, int count)
            : base(items, count)
        {

        }

    }

    public class ApplicationVersionSwaggerListQueryResultItem
    {
        public Guid Id { get; set; }

        public string ContentType { get; set; }

        public DateTimeOffset CreateDate { get; set; }

        public string Code { get; set; }

    }


    public class ApplicationVersionSwaggerListQueryHandler : IQueryHandler<ApplicationVersionSwaggerListQuery, ApplicationVersionSwaggerListQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public ApplicationVersionSwaggerListQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<ApplicationVersionSwaggerListQueryResult>> ExecuteAsync(ApplicationVersionSwaggerListQuery query)
        {
            var dbQuery = _queryModel
              .ApplicationVersionSwaggerSpecifications
              .Where(e => e.IdApplicationVersion == query.ApplicationVersionId)
              .Select(MappingDomainToQueryResult());

            var count = await dbQuery.CountAsync();
            var items = await dbQuery.ToArrayAsync();

            var result = new ApplicationVersionSwaggerListQueryResult(items, count);

            return OperationResult.Success(result);
        }
        internal static Expression<Func<ApplicationVersionSwaggerSpecificationEntity, ApplicationVersionSwaggerListQueryResultItem>> MappingDomainToQueryResult()
        {
            return e => new ApplicationVersionSwaggerListQueryResultItem
            {
                Code = e.Code,
                ContentType = e.ContentType,
                CreateDate = e.CreateDate,
                Id = e.Id,
            };
        }
    }
}
