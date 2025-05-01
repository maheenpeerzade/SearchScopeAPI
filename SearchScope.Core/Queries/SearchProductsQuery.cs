using MediatR;
using SearchScopeAPI.SearchScope.Core.Models;
using SearchScopeAPI.SearchScope.Core.Utility;

namespace SearchScopeAPI.SearchScope.Core.Queries
{
    /// <summary>
    /// SearchProductsQuery class.
    /// </summary>
    public class SearchProductsQuery : IRequest<IEnumerable<Product>>
    {
        /// <summary>
        /// Query.
        /// </summary>
        public string? Query { get; }

        /// <summary>
        /// Filter.
        /// </summary>
        public string? Filter { get; }

        /// <summary>
        /// Sort by.
        /// </summary>
        public ProductEnum? SortBy { get; }

        /// <summary>
        /// Is ascending.
        /// </summary>
        public bool IsAscending { get; }

        /// <summary>
        /// Page number.
        /// </summary>
        public int PageNumber { get; }

        /// <summary>
        /// page size.
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="query">Specify query.</param>
        /// <param name="filter">Specify filter.</param>
        /// <param name="sortBy">Specify sort by.</param>
        /// <param name="isAscending">Specify is ascending.</param>
        /// <param name="pageNumber">Specify page number.</param>
        /// <param name="pageSize">Specify page size.</param>
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
