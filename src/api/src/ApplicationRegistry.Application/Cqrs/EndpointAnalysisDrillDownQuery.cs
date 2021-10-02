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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Queries
{
    public class EndpointAnalysisDrillDownQuery : ListQueryParameters, IQuery
    {
        public Guid IdVersion { get; set; }

        public string HttpMethod { get; set; }

        public string Path { get; set; }
    }

    public class EndpointAnalysisDrillDownQueryValidator : AbstractValidator<EndpointAnalysisDrillDownQuery>
    {
        public EndpointAnalysisDrillDownQueryValidator()
        {

        }
    }

    public class EndpointAnalysisDrillDownQueryResult : CollectionQueryResultBase<EndpointAnalysisDrillDownQueryResultItem>
    {
        public EndpointAnalysisDrillDownQueryResult(IEnumerable<EndpointAnalysisDrillDownQueryResultItem> items, int count)
            : base(items, count)
        {

        }

    }

    public class EndpointAnalysisDrillDownQueryResultItem
    {
        public int Level { get; set; }

        public string ApplicationCode { get; set; }

        public string HttpMethod { get; set; }

        public string Path { get; set; }

        public bool IsLeaf { get; set; } = false;
    }


    public class EndpointAnalysisDrillDownQueryHandler : IQueryHandler<EndpointAnalysisDrillDownQuery, EndpointAnalysisDrillDownQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public EndpointAnalysisDrillDownQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<EndpointAnalysisDrillDownQueryResult>> ExecuteAsync(EndpointAnalysisDrillDownQuery query)
        {
            var app = await _queryModel
                .ApplicationVersions
                .Include(a => a.Application)
                .Where(predicate => predicate.Id == query.IdVersion)
                .Select(e => new { ApplicationCode = e.Application.Code, IdEnvironment = e.IdEnvironment })
                .FirstOrDefaultAsync();


            if (app == null)
                return OperationResult.Success(default(EndpointAnalysisDrillDownQueryResult));

            var q = from e in _queryModel.EndpointDependencies
                    from ed in _queryModel.EndpointDependencies
                    where
                        e.ApplicationCode == app.ApplicationCode
                        && e.EnvironmentId == app.IdEnvironment
                        && e.HttpMethod == query.HttpMethod
                        && e.Path == query.Path
                        && ed.Id.IsDescendantOf(e.Id)
                    select ed;

            var dbQuery = q
              .Select(MappingDomainToQueryResult());

            var count = await dbQuery.CountAsync();
            var items = await dbQuery.ToArrayAsync();
            
            for (int i = 0; i < items.Length -1; i++)
            {
                
                var item = items[i];
                var nextItem = items[i + 1];
                if (item.Level >= nextItem.Level)
                    item.IsLeaf = true;
            }

            items[items.Length - 1].IsLeaf = true;

            var result = new EndpointAnalysisDrillDownQueryResult(items, count);



            return OperationResult.Success(result);
        }
        internal static Expression<Func<EndpointDependencies, EndpointAnalysisDrillDownQueryResultItem>> MappingDomainToQueryResult()
        {
            return e => new EndpointAnalysisDrillDownQueryResultItem
            {
                ApplicationCode = e.ApplicationCode,
                HttpMethod = e.HttpMethod,
                Level = e.Id.GetLevel(),
                Path = e.Path
            };
        }
    }
}
