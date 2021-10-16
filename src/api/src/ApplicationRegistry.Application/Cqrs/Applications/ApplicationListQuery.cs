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

namespace ApplicationRegistry.Application.Queries.ApplicationsList
{
    public class ApplicationsListQuery : ListQueryParameters, IQuery
    {

    }

    public class ApplicationsListQueryValidator : AbstractValidator<ApplicationsListQuery>
    {
        public ApplicationsListQueryValidator()
        {

        }
    }

    public class ApplicationsListQueryResult : CollectionQueryResultBase<ApplicationsListQueryResultItem>
    {
        public ApplicationsListQueryResult(IEnumerable<ApplicationsListQueryResultItem> items, int count)
            : base(items, count)
        {

        }

    }

    public class ApplicationsListQueryResultItem
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public Guid ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string Description { get; set; }

        public string Owner { get; set; }

        public string RepositoryUrl { get; set; }

        public string BuildProcessUrls { get; set; }

        public DateTime CreateDate { get; set; }

        public string Framework { get; set; }
    }

    public class ApplicationsListQueryHandler : IQueryHandler<ApplicationsListQuery, ApplicationsListQueryResult>
    {
        readonly IQueryDataModel _uow;

        public ApplicationsListQueryHandler(IQueryDataModel uow)
        {
            _uow = uow;
        }

        public async Task<OperationResult<ApplicationsListQueryResult>> ExecuteAsync(ApplicationsListQuery query)
        {
            var applicationsQuery = _uow
                .Applications
                .Select(MappingDomainToQueryResult());

            var count = await applicationsQuery.CountAsync();

            applicationsQuery = applicationsQuery.SortAndPage(query);

            var items = await applicationsQuery.ToArrayAsync();

            var result = new ApplicationsListQueryResult(items, count);

            return OperationResult.Success(result);
        }



        internal static Expression<Func<ApplicationEntity, ApplicationsListQueryResultItem>> MappingDomainToQueryResult()
        {
            return e => new ApplicationsListQueryResultItem
            {
                Id = e.Id,
                BuildProcessUrls = e.BuildProcessUrls,
                CreateDate = e.CreateDate,
                Description = e.Description,
                Name = e.Name,
                Owner = e.Owner,
                ProjectName = e.Project.Name,
                ProjectId = e.IdProject,
                RepositoryUrl = e.RepositoryUrl,
                Code = e.Code,
                Framework = e.Framework ?? "",
            };
        }
    }
}
