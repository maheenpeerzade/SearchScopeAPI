using SearchScopeAPI.SearchScope.Core.Models;

namespace SearchScopeAPI.SearchScope.Core.Interfaces
{
    public interface ISearchResultRepository
    {
        Task<IEnumerable<SearchResult>> GetSearchResultsAsync(int searchHistoryId);
    }
}
