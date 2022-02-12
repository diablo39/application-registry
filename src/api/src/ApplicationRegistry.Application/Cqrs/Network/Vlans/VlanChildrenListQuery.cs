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
using System.Net;


namespace ApplicationRegistry.Application.Cqrs.Network.Vlans
{
    public class VlanChildrenListQuery : ListQueryParameters, IQuery
    {
        public Guid? Id { get; set; }
    }

    public class VlanChildrenListQueryValidator : AbstractValidator<VlanChildrenListQuery>
    {
        public VlanChildrenListQueryValidator()
        {

        }
    }

    public class VlanChildrenListQueryResult : CollectionQueryResultBase<VlanChildrenListQueryResultItem>
    {
        public VlanChildrenListQueryResult(IEnumerable<VlanChildrenListQueryResultItem> items, int count)
            : base(items, count)
        {

        }

    }

    public class VlanChildrenListQueryResultItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int? Number { get; set; }

        public string Alias { get; set; }

        public string Cidr { get; set; }

        public string Description { get; set; }

        public string RFC { get; set; }

        public DateTimeOffset CreateDate { get; set; }

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


    public class VlanChildrenListQueryHandler : IQueryHandler<VlanChildrenListQuery, VlanChildrenListQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public VlanChildrenListQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<VlanChildrenListQueryResult>> ExecuteAsync(VlanChildrenListQuery query)
        {
            var item = await _queryModel.Vlans.FirstOrDefaultAsync(e => e.Id == query.Id);

            if (item == null) return OperationResult.Success<VlanChildrenListQueryResult>(null);


            var vlan = IPNetwork.Parse(item.Cidr);

            var start = item.Cidr.Split('/')[0].ConvertIpToHexString();
            var end = vlan.LastUsable.ToString().ConvertIpToHexString();

            var dbQuery = _queryModel
              .Vlans
              .Where(e => 1 == 1
                  && e.Id != query.Id.Value
                  && e.IpAsHexString.CompareTo(start) >= 0
                  && e.IpAsHexString.CompareTo(end) <= 0
                  && Convert.ToByte(e.Cidr.Substring(e.Cidr.IndexOf("/") + 1)) > vlan.Cidr)
              .Select(MappingDomainToQueryResult());

            var count = await dbQuery.CountAsync();
            var items = await dbQuery.ToArrayAsync();

            var result = new VlanChildrenListQueryResult(items, count);

            return OperationResult.Success(result);
        }
        internal static Expression<Func<VlanEntity, VlanChildrenListQueryResultItem>> MappingDomainToQueryResult()
        {
            return e => new VlanChildrenListQueryResultItem
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
