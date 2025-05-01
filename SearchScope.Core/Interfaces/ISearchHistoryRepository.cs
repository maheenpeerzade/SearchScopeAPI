using SearchScopeAPI.SearchScope.Core.Models;

namespace SearchScopeAPI.SearchScope.Core.Interfaces
{
    /// <summary>
    /// ISearchHistoryRepository interface.
    /// </summary>
    public interface ISearchHistoryRepository
    {
        /// <summary>
        /// To get search histories by user id.
        /// </summary>
        /// <param name="userId">Specify user id.</param>
        /// <param name="isAscending">Specify isAscending.</param>
        /// <returns>SearchHistories.</returns>
        Task<IEnumerable<SearchHistory>> GetSearchHistoriesAsync(int userId, bool isAscending = true);
    }
}
