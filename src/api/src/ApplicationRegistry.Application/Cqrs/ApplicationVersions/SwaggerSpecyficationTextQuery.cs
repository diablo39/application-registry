using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ApplicationRegistry.Application.Queries
{
    public class SwaggerSpecyficationTextQuery : IQuery
    {
        public Guid ApplicationVersionId { get; set; }
    }

    public class SwaggerSpecyficationTextQueryValidator : AbstractValidator<SwaggerSpecyficationTextQuery>
    {
        public SwaggerSpecyficationTextQueryValidator()
        {

        }
    }

    public class SwaggerSpecyficationTextQueryResult
    {
        public string Text { get; set; }

        public string ContentType { get; set; }
    }



    public class SwaggerSpecyficationTextQueryHandler : IQueryHandler<SwaggerSpecyficationTextQuery, SwaggerSpecyficationTextQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public SwaggerSpecyficationTextQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<SwaggerSpecyficationTextQueryResult>> ExecuteAsync(SwaggerSpecyficationTextQuery query)
        {
            SwaggerSpecyficationTextQueryResult result = null;

            var applicationVersionSpecificationEntity = await _queryModel.ApplicationVersionSwaggerSpecifications
                .FirstOrDefaultAsync(m => m.IdApplicationVersion == query.ApplicationVersionId);

            if (applicationVersionSpecificationEntity == null)
            {
                return OperationResult.Success(result);
            }

            result = new SwaggerSpecyficationTextQueryResult
            {
                ContentType = applicationVersionSpecificationEntity.ContentType,
                Text = applicationVersionSpecificationEntity.Specification
            };

            return OperationResult.Success(result);
        }
    }
}
