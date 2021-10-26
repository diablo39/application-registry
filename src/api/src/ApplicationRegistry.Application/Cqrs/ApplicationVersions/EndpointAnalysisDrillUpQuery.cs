using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Queries
{
    public class EndpointAnalysisDrillUpQuery : ListQueryParameters, IQuery
    {
        public Guid IdVersion { get; set; }

        public string HttpMethod { get; set; }

        public string Path { get; set; }
    }

    public class EndpointAnalysisDrillUpQueryValidator : AbstractValidator<EndpointAnalysisDrillUpQuery>
    {
        public EndpointAnalysisDrillUpQueryValidator()
        {

        }
    }

    public class EndpointAnalysisDrillUpQueryResult : CollectionQueryResultBase<EndpointAnalysisDrillUpQueryResultItem>
    {
        public EndpointAnalysisDrillUpQueryResult(IEnumerable<EndpointAnalysisDrillUpQueryResultItem> items, int count)
            : base(items, count)
        {

        }

    }

    public class EndpointAnalysisDrillUpQueryResultItem
    {
        public int Level { get; set; }

        public string ApplicationCode { get; set; }

        public string HttpMethod { get; set; }

        public string Path { get; set; }

        public bool IsLeaf { get; set; } = false;
    }


    public class EndpointAnalysisDrillUpQueryHandler : IQueryHandler<EndpointAnalysisDrillUpQuery, EndpointAnalysisDrillUpQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public EndpointAnalysisDrillUpQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<EndpointAnalysisDrillUpQueryResult>> ExecuteAsync(EndpointAnalysisDrillUpQuery query)
        {
            var app = await _queryModel
                .ApplicationVersions
                .Include(a => a.Application)
                .Where(predicate => predicate.Id == query.IdVersion)
                .Select(e => new { ApplicationCode = e.Application.Code, IdEnvironment = e.IdEnvironment })
                .FirstOrDefaultAsync();


            if (app == null)
                return OperationResult.Success(default(EndpointAnalysisDrillUpQueryResult));

            var q = from e in _queryModel.EndpointDependencies
                    from ed in _queryModel.EndpointDependencies
                    where
                        e.ApplicationCode == app.ApplicationCode
                        && e.EnvironmentId == app.IdEnvironment
                        && e.HttpMethod == query.HttpMethod
                        && e.Path == query.Path
                        && e.Id.IsDescendantOf(ed.Id)
                    select ed;

            var dbQuery = q
              .Select(MappingDomainToQueryResult());

            var count = await dbQuery.CountAsync();
            var items = await dbQuery.ToArrayAsync();

            for (int i = 0; i < items.Length - 1; i++)
            {

                var item = items[i];
                var nextItem = items[i + 1];
                if (item.Level >= nextItem.Level)
                    item.IsLeaf = true;
            }

            items[items.Length - 1].IsLeaf = true;

            var result = new EndpointAnalysisDrillUpQueryResult(items, count);

            return OperationResult.Success(result);
        }
        internal static Expression<Func<EndpointDependencies, EndpointAnalysisDrillUpQueryResultItem>> MappingDomainToQueryResult()
        {

            return e => new EndpointAnalysisDrillUpQueryResultItem
            {
                ApplicationCode = e.ApplicationCode,
                HttpMethod = e.HttpMethod,
                Level = e.Id.GetLevel(),
                Path = e.Path
            };

        }
    }
}
