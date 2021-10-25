using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Cqrs.Environments
{
    public class EnvironmentDetailsQuery : IQuery
    {
        public string Id { get; set; }
    }

    public class EnvironmentDetailsQueryValidator : AbstractValidator<EnvironmentDetailsQuery>
    {
        public EnvironmentDetailsQueryValidator()
        {
            RuleFor(e => e.Id).NotEmpty();
        }
    }

    public class EnvironmentDetailsQueryResult
    {
        public string Id { get; set; }
        public string Description { get; set; }

        public DateTime CreateDate { get; set; }
    }



    public class EnvironmentDetailsQueryHandler : IQueryHandler<EnvironmentDetailsQuery, EnvironmentDetailsQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public EnvironmentDetailsQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<EnvironmentDetailsQueryResult>> ExecuteAsync(EnvironmentDetailsQuery query)
        {
            EnvironmentDetailsQueryResult result = null;

            result = await _queryModel.Environments.Where(e => e.Id == query.Id).Select(MappingDomainToQueryResult()).SingleOrDefaultAsync();

            return OperationResult.Success(result);
        }

        internal static Expression<Func<EnvironmentEntity, EnvironmentDetailsQueryResult>> MappingDomainToQueryResult()
        {
            return e => new EnvironmentDetailsQueryResult
            {
                Description = e.Description,
                Id = e.Id,
                CreateDate = e.CreateDate
            };
        }
    }
}
