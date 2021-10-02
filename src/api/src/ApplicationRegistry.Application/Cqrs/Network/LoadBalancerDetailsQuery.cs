using ApplicationRegistry.Application.Services;
using ApplicationRegistry.CQRS.Abstraction;
using FluentValidation;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Queries
{
    public class LoadBalancerDetailsQuery : IQuery
    {
        public string Id { get; set; }

    }

    public class LoadBalancerDetailsQueryValidator : AbstractValidator<LoadBalancerDetailsQuery>
    {
        public LoadBalancerDetailsQueryValidator()
        {

        }
    }



    public class LoadBalancerDetailsQueryHandler : IQueryHandler<LoadBalancerDetailsQuery, object>
    {
        readonly SotDataProvider _dataProvider;

        public LoadBalancerDetailsQueryHandler(SotDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public async Task<OperationResult<object>> ExecuteAsync(LoadBalancerDetailsQuery query)
        {
            var details = await _dataProvider.GetLoadBalancerDetailsAsync(query.Id);

            return OperationResult.Success(details);
        }
    }
}
