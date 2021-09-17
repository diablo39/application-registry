using ApplicationRegistry.Application.Services;
using ApplicationRegistry.CQRS.Abstraction;
using FluentValidation;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Queries
{
    public class FirewallRuleDetailsQuery : IQuery
    {
        public string Id { get; set; }

    }

    public class FirewallRuleDetailsQueryValidator : AbstractValidator<FirewallRuleDetailsQuery>
    {
        public FirewallRuleDetailsQueryValidator()
        {

        }
    }



    public class FirewallRuleDetailsQueryHandler : IQueryHandler<FirewallRuleDetailsQuery, object>
    {
        readonly SotDataProvider _dataProvider;

        public FirewallRuleDetailsQueryHandler(SotDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public async Task<OperationResult<object>> ExecuteAsync(FirewallRuleDetailsQuery query)
        {
            var details = await _dataProvider.GetFirewallRuleDetailsAsync(query.Id);

            return OperationResult.Success(details);
        }
    }
}
