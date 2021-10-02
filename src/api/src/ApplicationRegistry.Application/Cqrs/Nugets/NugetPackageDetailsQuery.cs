using ApplicationRegistry.Application.Queries;
using ApplicationRegistry.CQRS.Abstraction;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Domain.Entities;
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
    public class NugetPackageDetailsQuery : IQuery
    {
        public long? Id { get; set; }
    }

    public class NugetPackageDetailsQueryValidator : AbstractValidator<NugetPackageDetailsQuery>
    {
        public NugetPackageDetailsQueryValidator()
        {

        }
    }

    public class NugetPackageDetailsQueryResult
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime CreateDate { get; set; }
    }



    public class NugetPackageDetailsQueryHandler : IQueryHandler<NugetPackageDetailsQuery, NugetPackageDetailsQueryResult>
    {
        readonly IQueryDataModel _queryModel;

        public NugetPackageDetailsQueryHandler(IQueryDataModel queryModel)
        {
            _queryModel = queryModel;
        }

        public async Task<OperationResult<NugetPackageDetailsQueryResult>> ExecuteAsync(NugetPackageDetailsQuery query)
        {
            NugetPackageDetailsQueryResult result = await _queryModel.NugetPackages.Where(e=> e.Id == query.Id).Select(MappingDomainToQueryResult()).FirstOrDefaultAsync();

            return OperationResult.Success(result);
        }

        internal static Expression<Func<NugetPackageEntity, NugetPackageDetailsQueryResult>> MappingDomainToQueryResult()
        {
            return e => new NugetPackageDetailsQueryResult
            {
                CreateDate = e.CreateDate,
                Id = e.Id,
                Name = e.Name,
            };
        }
    }
}