using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Cqrs.Systems
{
    public class SystemDetailsQuery : IQuery
    {
        public Guid Id { get; set; }
    }

    public class SystemDetailsQueryValidator : AbstractValidator<SystemDetailsQuery>
    {
        public SystemDetailsQueryValidator()
        {

        }
    }

    public class SystemDetailsQueryResult
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreateDate { get; set; }
    }



    public class SystemDetailsQueryHandler : IQueryHandler<SystemDetailsQuery, SystemDetailsQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public SystemDetailsQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<SystemDetailsQueryResult>> ExecuteAsync(SystemDetailsQuery query)
        {
            SystemDetailsQueryResult result = null;

            result = await _queryModel.Systems.Where(e => e.Id == query.Id).Select(MappingDomainToQueryResult()).SingleOrDefaultAsync();

            return OperationResult.Success(result);
        }

        internal static Expression<Func<SystemEntity, SystemDetailsQueryResult>> MappingDomainToQueryResult()
        {
            return e => new SystemDetailsQueryResult
            {
                Id = e.Id,
                Description = e.Description,
                Name = e.Name,
                CreateDate = e.CreateDate
            };
        }
    }
}
