using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationRegistry.Application.Cqrs.ApplicationVersions
{
    public class ApplicationVersionSwaggerSpecificationTextQueryQuery : IQuery
    {
        public Guid ApplicationVersionId { get; set; }

        public Guid Id { get; set; }
    }

    public class ApplicationVersionSwaggerSpecificationTextQueryQueryValidator : AbstractValidator<ApplicationVersionSwaggerSpecificationTextQueryQuery>
    {
        public ApplicationVersionSwaggerSpecificationTextQueryQueryValidator()
        {
        }
    }

    public class ApplicationVersionSwaggerSpecificationTextQueryQueryResult
    {
        public string Text { get; set; }

        public string ContentType { get; set; }
    }

    public class ApplicationVersionSwaggerSpecificationTextQueryQueryHandler : IQueryHandler<ApplicationVersionSwaggerSpecificationTextQueryQuery, ApplicationVersionSwaggerSpecificationTextQueryQueryResult>
    {
        private readonly IQueryDataModel _queryModel;

        public ApplicationVersionSwaggerSpecificationTextQueryQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<ApplicationVersionSwaggerSpecificationTextQueryQueryResult>> ExecuteAsync(ApplicationVersionSwaggerSpecificationTextQueryQuery query)
        {
            ApplicationVersionSwaggerSpecificationTextQueryQueryResult result = null;

            var applicationVersionSpecificationEntity = await _queryModel.ApplicationVersionSwaggerSpecifications
                .FirstOrDefaultAsync(m => m.IdApplicationVersion == query.ApplicationVersionId && m.Id == query.Id);

            if (applicationVersionSpecificationEntity == null)
            {
                return OperationResult.Success(result);
            }

            result = new ApplicationVersionSwaggerSpecificationTextQueryQueryResult
            {
                ContentType = applicationVersionSpecificationEntity.ContentType,
                Text = applicationVersionSpecificationEntity.Specification
            };

            return OperationResult.Success(result);
        }
    }
}