using Microsoft.EntityFrameworkCore;
using SearchScopeAPI.SearchScope.Core.Interfaces;
using SearchScopeAPI.SearchScope.Core.Models;
using SearchScopeAPI.SearchScope.DataAccess.Data;

namespace SearchScopeAPI.SearchScope.DataAccess.Repositories
{
    /// <summary>
    /// SearchHistoryRepository class.
    /// </summary>
    public class SearchHistoryRepository : ISearchHistoryRepository
    {
        private readonly SearchScopeDbContext _context;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">Specify SearchScopeDbContext.</param>
        public SearchHistoryRepository(SearchScopeDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// To get search histories by user id.
        /// </summary>
        /// <param name="userId">Specify user id.</param>
        /// <param name="isAscending">Specify isAscending.</param>
        /// <returns>SearchHistories.</returns>
        public async Task<IEnumerable<SearchHistory>> GetSearchHistoriesAsync(int userId, bool isAscending = true)
        {
            if (isAscending)
            {
                return await _context.SearchHistories.Where(sh => sh.UserId == userId).OrderBy(sh => sh.SearchDate).ToListAsync();
            }
            else
            {
                return await _context.SearchHistories.Where(sh => sh.UserId == userId).OrderByDescending(sh => sh.SearchDate).ToListAsync();
            }
        }

    }
}
