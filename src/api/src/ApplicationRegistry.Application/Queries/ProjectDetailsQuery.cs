using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Queries
{
    public class ProjectDetailsQuery : IQuery
    {
        public Guid Id { get; set; }
    }

    public class ProjectDetailsQueryValidator : AbstractValidator<ProjectDetailsQuery>
    {
        public ProjectDetailsQueryValidator()
        {

        }
    }

    public class ProjectDetailsQueryResult
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }
    }



    public class ProjectDetailsQueryHandler : IQueryHandler<ProjectDetailsQuery, ProjectDetailsQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public ProjectDetailsQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<ProjectDetailsQueryResult>> ExecuteAsync(ProjectDetailsQuery query)
        {
            ProjectDetailsQueryResult result = null;

            result = await _queryModel.Projects.Where(e => e.Id == query.Id).Select(MappingDomainToQueryResult()).SingleOrDefaultAsync();

            return OperationResult.Success(result);
        }

        internal static Expression<Func<ProjectEntity, ProjectDetailsQueryResult>> MappingDomainToQueryResult()
        {
            return e => new ProjectDetailsQueryResult
            {
                Id = e.Id,
                Description = e.Description,
                Name = e.Name,
                CreateDate = e.CreateDate
            };
        }
    }
}
