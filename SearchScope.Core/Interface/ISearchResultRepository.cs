using SearchScopeAPI.SearchScope.Core.Models;

namespace SearchScopeAPI.SearchScope.Core.Interface
{
    public interface ISearchResultRepository
    {
        Task<IEnumerable<SearchResult>> GetSearchResultsAsync(int searchHistoryId);
    }
}
