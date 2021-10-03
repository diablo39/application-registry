using ApplicationRegistry.Application.Services;
using ApplicationRegistry.CQRS.Abstraction;
using FluentValidation;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Queries
{
    public class MachineDetailsQuery : IQuery
    {
        public string Id { get; set; }

    }

    public class MachineDetailsQueryValidator : AbstractValidator<MachineDetailsQuery>
    {
        public MachineDetailsQueryValidator()
        {

        }
    }



    public class MachineDetailsQueryHandler : IQueryHandler<MachineDetailsQuery, object>
    {
        readonly SotDataProvider _dataProvider;

        public MachineDetailsQueryHandler(SotDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public async Task<OperationResult<object>> ExecuteAsync(MachineDetailsQuery query)
        {
            var details = await _dataProvider.GetMachineDetailsAsync(query.Id);

            return OperationResult.Success(details);
        }
    }
}
