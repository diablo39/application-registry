using ApplicationRegistry.Application.Services;
using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Domain.Entities.Network;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Queries
{
    public class LoadBalancerDetailsQuery : IQuery
    {
        public Guid? Id { get; set; }

    }

    public class LoadBalancerDetailsQueryValidator : AbstractValidator<LoadBalancerDetailsQuery>
    {
        public LoadBalancerDetailsQueryValidator()
        {

        }
    }

    public class LoadBalancerDetailsQueryResult
    {
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        public string Name { get; set; }

        public string Ip { get; set; }

        public string Port { get; set; }

        public string Description { get; set; }

        public string Fqdn { get; set; }
    }

    public class LoadBalancerDetailsQueryHandler : IQueryHandler<LoadBalancerDetailsQuery, LoadBalancerDetailsQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public LoadBalancerDetailsQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<LoadBalancerDetailsQueryResult>> ExecuteAsync(LoadBalancerDetailsQuery query)
        {
            LoadBalancerDetailsQueryResult result = null;

            result = await _queryModel.LoadBalancers.Where(e => e.Id == query.Id).Select(MappingDomainToQueryResult()).SingleOrDefaultAsync();

            return OperationResult.Success(result);
        }

        internal static Expression<Func<LoadBalancerEntity, LoadBalancerDetailsQueryResult>> MappingDomainToQueryResult()
        {
            return e => new LoadBalancerDetailsQueryResult
            {
                CreateDate = e.CreateDate,
                Description = e.Description,
                Fqdn = e.Fqdn,
                Id = e.Id,
                Ip = e.Ip,
                Name = e.Name,
                Port = e.Port
            };
        }
    }
}
