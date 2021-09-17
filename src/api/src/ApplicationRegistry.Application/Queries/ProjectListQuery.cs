using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Queries
{
    public class ProjectsListQuery : IQuery
    {

    }

    public class ProjectsListQueryValidator : AbstractValidator<ProjectsListQuery>
    {
        public ProjectsListQueryValidator()
        {

        }
    }

    public class ProjectsListQueryResult : CollectionQueryResultBase<ProjectsListQueryResultItem>
    {
        public ProjectsListQueryResult(IEnumerable<ProjectsListQueryResultItem> items, int count)
            : base(items, count)
        {

        }

    }

    public class ProjectsListQueryResultItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }
    }


    public class ProjectsListQueryHandler : IQueryHandler<ProjectsListQuery, ProjectsListQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public ProjectsListQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<ProjectsListQueryResult>> ExecuteAsync(ProjectsListQuery query)
        {
            var dbQuery = _queryModel
              .Projects
              .Select(MappingDomainToQueryResult());

            var count = await dbQuery.CountAsync();
            var items = await dbQuery.ToArrayAsync();

            var result = new ProjectsListQueryResult(items, count);

            return OperationResult.Success(result);
        }
        internal static Expression<Func<ProjectEntity, ProjectsListQueryResultItem>> MappingDomainToQueryResult()
        {
            return e => new ProjectsListQueryResultItem
            {
                CreateDate = e.CreateDate,
                Description = e.Description,
                Id = e.Id,
                Name = e.Name

            };
        }
    }
}
