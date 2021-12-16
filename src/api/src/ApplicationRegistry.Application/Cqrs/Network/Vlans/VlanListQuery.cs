using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Domain.Entities;
using ApplicationRegistry.Domain.Entities.Network;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRegistry.Application.Cqrs.Network.Vlans
{
    public class VlanListQuery : ListQueryParameters, IQuery
    {

    }

    public class VlanListQueryValidator : AbstractValidator<VlanListQuery>
    {
        public VlanListQueryValidator()
        {

        }
    }

    public class VlanListQueryResult : CollectionQueryResultBase<VlanListQueryResultItem>
    {
        public VlanListQueryResult(IEnumerable<VlanListQueryResultItem> items, int count)
            : base(items, count)
        {

        }

    }

    public class VlanListQueryResultItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int? Number { get; set; }

        public string Alias { get; set; }

        public string Cidr { get; set; }

        public string Description { get; set; }

        public string RFC { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsVirtualDirectory { get; set; }

        public string CidrSortField
        {
            get
            {
                var parts = this.Cidr.Split('/');
                var ip = parts[0].ConvertIpToHexString();
                var cidr = parts[1].ConvertIpToHexString();
                return String.Concat(ip, '/', cidr);
            }
        }

    }


    public class VlanListQueryHandler : IQueryHandler<VlanListQuery, VlanListQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public VlanListQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<VlanListQueryResult>> ExecuteAsync(VlanListQuery query)
        {
            var dbQuery = _queryModel
              .Vlans
              .Select(MappingDomainToQueryResult());

            var count = await dbQuery.CountAsync();
            var items = await dbQuery.SortAndPage(query).ToArrayAsync();

            var result = new VlanListQueryResult(items, count);

            return OperationResult.Success(result);
        }
        internal static Expression<Func<VlanEntity, VlanListQueryResultItem>> MappingDomainToQueryResult()
        {
            return e => new VlanListQueryResultItem
            {
                Alias = e.Alias,
                Cidr = e.Cidr,
                CreateDate = e.CreateDate,
                Description = e.Description,
                Id = e.Id,
                Name = e.Name,
                Number = e.Number,
                RFC = e.RFC,
                IsVirtualDirectory = e.IsVirtualDirectory,
            };
        }
    }
}
