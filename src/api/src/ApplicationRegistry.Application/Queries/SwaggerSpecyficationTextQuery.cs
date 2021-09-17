using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
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

            var applicationVersionSpecificationEntity = await _queryModel.ApplicationVersionSpecifications.Include(e => e.SpecificationText)
                .FirstOrDefaultAsync(m => m.IdApplicationVersion == query.ApplicationVersionId && m.SpecificationType == SpecificationTypeEntity.Swagger);
            if (applicationVersionSpecificationEntity == null)
            {
                return OperationResult.Success(result);
            }

            result = new SwaggerSpecyficationTextQueryResult
            {
                ContentType = applicationVersionSpecificationEntity.ContentType,
                Text = applicationVersionSpecificationEntity.SpecificationText.Specification
            };

            return OperationResult.Success(result);
        }
    }

    public static class SwaggerSpecyficationTextQueryRegistration
    {
        public static void RegisterSwaggerSpecyficationTextQuery(this IServiceCollection services)
        {
            services.AddTransient<IValidator<SwaggerSpecyficationTextQuery>, SwaggerSpecyficationTextQueryValidator>();
            services.AddScoped<IQueryHandler<SwaggerSpecyficationTextQuery, SwaggerSpecyficationTextQueryResult>, SwaggerSpecyficationTextQueryHandler>();
        }
    }
}
