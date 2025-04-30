using MediatR;
using SearchScopeAPI.SearchScope.Core.Models;
using SearchScopeAPI.SearchScope.Core.Utility;

namespace SearchScopeAPI.SearchScope.Core.Queries
{
    public class SearchProductsQuery : IRequest<IEnumerable<Product>>
    {
        public string? Query { get; }
        public string? Filter { get; }
        public ProductEnum? SortBy { get; }

        public bool IsAscending { get; }

        public int PageNumber { get; }
        public int PageSize { get; }

        public SearchProductsQuery(string? query, string? filter, ProductEnum? sortBy, bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            Query = query;
            Filter = filter;
            SortBy = sortBy;
            IsAscending = isAscending;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
