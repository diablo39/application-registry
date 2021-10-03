using ApplicationRegistry.Application.Services;
using ApplicationRegistry.CQRS.Abstraction;
using FluentValidation;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Queries
{
    public class VlanDetailsQuery : IQuery
    {
        public string Id { get; set; }

    }

    public class VlanDetailsQueryValidator : AbstractValidator<VlanDetailsQuery>
    {
        public VlanDetailsQueryValidator()
        {

        }
    }



    public class VlanDetailsQueryHandler : IQueryHandler<VlanDetailsQuery, object>
    {
        readonly SotDataProvider _dataProvider;

        public VlanDetailsQueryHandler(SotDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public async Task<OperationResult<object>> ExecuteAsync(VlanDetailsQuery query)
        {
            var details = await _dataProvider.GetVlanDetailsAsync(query.Id);

            return OperationResult.Success(details);
        }
    }
}
