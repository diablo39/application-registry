using System;

namespace ApplicationRegistry.CQRS.Abstraction
{
    public interface IListQueryParameters
    {
        int ItemsPerPage { get; set; }
        int Page { get; set; }
        string SortBy { get; set; }
        bool? SortDesc { get; set; }
    }
}
