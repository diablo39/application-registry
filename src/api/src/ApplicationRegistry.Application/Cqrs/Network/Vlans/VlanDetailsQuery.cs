using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Domain.Entities.Network;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Queries
{
    public class VlanDetailsQuery : IQuery
    {
        public Guid Id { get; set; }
    }

    public class VlanDetailsQueryValidator : AbstractValidator<VlanDetailsQuery>
    {
        public VlanDetailsQueryValidator()
        {

        }
    }

    public class VlanDetailsQueryResult
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int? Number { get; set; }

        public string Alias { get; set; }

        public string Cidr { get; set; }

        public string Description { get; set; }

        public string RFC { get; set; }

        public DateTime CreateDate { get; set; }
    }


    public class VlanDetailsQueryHandler : IQueryHandler<VlanDetailsQuery, VlanDetailsQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public VlanDetailsQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<VlanDetailsQueryResult>> ExecuteAsync(VlanDetailsQuery query)
        {
            VlanDetailsQueryResult result = null;

            result = await _queryModel.Vlans.Where(e => e.Id == query.Id).Select(MappingDomainToQueryResult()).SingleOrDefaultAsync();

            return OperationResult.Success(result);
        }

        internal static Expression<Func<VlanEntity, VlanDetailsQueryResult>> MappingDomainToQueryResult()
        {
            return e => new VlanDetailsQueryResult
            {
                Alias = e.Alias,
                Cidr = e.Cidr,
                CreateDate = e.CreateDate,
                Description = e.Description,
                Id = e.Id,
                Name = e.Name,
                Number = e.Number,
                RFC = e.RFC
            };
        }
    }


}
