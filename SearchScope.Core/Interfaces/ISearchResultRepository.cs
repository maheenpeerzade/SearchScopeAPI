using SearchScopeAPI.SearchScope.Core.Models;

namespace SearchScopeAPI.SearchScope.Core.Interfaces
{
    /// <summary>
    /// ISearchResultRepository interface.
    /// </summary>
    public interface ISearchResultRepository
    {
        /// <summary>
        /// To get search results by seach history id.
        /// </summary>
        /// <param name="searchHistoryId">Specify search history id.</param>
        /// <returns>SearchResults.</returns>
        Task<IEnumerable<SearchResult>> GetSearchResultsAsync(int searchHistoryId);
    }
}
