using ApplicationRegistry.Application.Services;
using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Queries
{
    public class MachineListQuery : ListQueryParameters, IQuery
    {

    }

    public class MachineListQueryValidator : AbstractValidator<MachineListQuery>
    {
        public MachineListQueryValidator()
        {

        }
    }

    public class MachineListQueryResult : CollectionQueryResultBase<MachineListItemModel>
    {
        public MachineListQueryResult(IEnumerable<MachineListItemModel> items, int count)
            : base(items, count)
        {

        }

    }



    public class MachineListQueryHandler : IQueryHandler<MachineListQuery, MachineListQueryResult>
    {
        readonly SotDataProvider _dataProvider;

        public MachineListQueryHandler(SotDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public async Task<OperationResult<MachineListQueryResult>> ExecuteAsync(MachineListQuery query)
        {
            var items = await _dataProvider.GetMachinesAsync();

            var result = new MachineListQueryResult(items, items.Count);

            return OperationResult.Success(result);
        }
    }

    public static class MachineListQueryRegistration
    {
        public static void RegisterMachineListQuery(this IServiceCollection services)
        {
            services.AddTransient<IValidator<MachineListQuery>, MachineListQueryValidator>();
            services.AddScoped<IQueryHandler<MachineListQuery, MachineListQueryResult>, MachineListQueryHandler>();
        }
    }
}
