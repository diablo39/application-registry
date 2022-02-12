using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ApplicationRegistry.CQRS.Abstraction;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Queries
{
    public class ApplicationDetailsQuery : IQuery
    {
        public virtual string IdOrCode { get; set; }
    }

    public class ApplicationDetailsQueryValidator : AbstractValidator<ApplicationDetailsQuery>
    {
        public ApplicationDetailsQueryValidator()
        {
            RuleFor(e => e.IdOrCode).NotNull().NotEmpty();
        }
    }

    public class ApplicationDetailsQueryResult
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public DateTimeOffset CreateDate { get; set; }

        public string Description { get; set; }

        public string Owner { get; set; }

        public string RepositoryUrl { get; set; }

        public string BuildProcessUrls { get; set; }

        public Guid ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string Framework { get; set; }

        public IEnumerable<ApplicationDetailsQueryResultEndpoint> Endpoints { get; set; }

    }

    public class ApplicationDetailsQueryResultEndpoint
    {
        public string EnvironmentId { get; internal set; }

        public string Path { get; internal set; }

        public string Comment { get; internal set; }

        public Guid Id { get; internal set; }
    }

    public class ApplicationDetailsQueryHandler : IQueryHandler<ApplicationDetailsQuery, ApplicationDetailsQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public ApplicationDetailsQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<ApplicationDetailsQueryResult>> ExecuteAsync(ApplicationDetailsQuery query)
        {
            var dbQuery = _queryModel.Applications
                .Include(a => a.System);


            var applicationEntityResult = await dbQuery.Where(m => m.Id.ToString() == query.IdOrCode || m.Code == query.IdOrCode)
                .Select(applicationEntity => new ApplicationDetailsQueryResult
                {
                    BuildProcessUrls = applicationEntity.BuildProcessUrls,
                    Code = applicationEntity.Code,
                    CreateDate = applicationEntity.CreateDate,
                    Description = applicationEntity.Description,
                    Id = applicationEntity.Id,
                    Name = applicationEntity.Name,
                    Owner = applicationEntity.Owner,
                    ProjectId = applicationEntity.System.Id,
                    ProjectName = applicationEntity.System.Name,
                    RepositoryUrl = applicationEntity.RepositoryUrl,
                    Framework = applicationEntity.Framework,
                    Endpoints = applicationEntity.Endpoints.Select(e => new ApplicationDetailsQueryResultEndpoint
                    {
                        Comment = e.Comment,
                        EnvironmentId = e.EnvironmentId,
                        Path = e.Path,
                        Id = e.Id
                    })
                }).SingleOrDefaultAsync();

            if (applicationEntityResult == null)
            {
                return OperationResult.Success(default(ApplicationDetailsQueryResult));
            }
            return OperationResult.Success(applicationEntityResult);
        }
    }
}
