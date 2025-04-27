using SearchScopeAPI.SearchScope.Core.Models;

namespace SearchScopeAPI.SearchScope.Core.Interface
{
    public interface ISearchHistoryRepository
    {
        Task<IEnumerable<SearchHistory>> GetSearchHistoriesAsync(int userId, bool isAscending = true);
    }
}
