using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.CQRS.Abstraction
{
    public class ListQueryParameters : IListQueryParameters
    {
        public string SortBy { get; set; } = null;

        public int ItemsPerPage { get; set; } = -1;
        public int Page { get; set; } = 1;
        public bool? SortDesc { get; set; } = null;

        public void AssignListQueryParameters(ListQueryParameters parameters)
        {
            this.ItemsPerPage = parameters.ItemsPerPage;
            this.Page = parameters.Page;
            this.SortBy = parameters.SortBy;
            this.SortDesc = parameters.SortDesc;
        }
    }
}
