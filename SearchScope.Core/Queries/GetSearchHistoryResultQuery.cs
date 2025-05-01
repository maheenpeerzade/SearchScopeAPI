using MediatR;
using SearchScopeAPI.SearchScope.Core.Models;
using SearchScopeAPI.SearchScope.Core.Utility;

namespace SearchScopeAPI.SearchScope.Core.Queries
{
    /// <summary>
    /// GetSearchHistoryResultQuery class.
    /// </summary>
    public class GetSearchHistoryResultQuery : IRequest<IEnumerable<SearchResult>>
    {
        /// <summary>
        /// Search history id.
        /// </summary>
        public int SearchHistoryId { get; }

        /// <summary>
        /// Sort by.
        /// </summary>
        public SearchResultEnum? SortBy { get; }

        /// <summary>
        /// Is ascending.
        /// </summary>
        public bool IsAscending { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="searchHistoryId">Specify search history id.</param>
        /// <param name="sortBy">Specify sort by.</param>
        /// <param name="isAscending">Specify is ascending.</param>
        public GetSearchHistoryResultQuery(int searchHistoryId, SearchResultEnum? sortBy, bool isAscending)
        {
            SearchHistoryId = searchHistoryId;
            SortBy = sortBy;
            IsAscending = isAscending;
        }
    }
}
