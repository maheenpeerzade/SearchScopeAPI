using Microsoft.EntityFrameworkCore;
using SearchScopeAPI.SearchScope.Core.Interface;
using SearchScopeAPI.SearchScope.Core.Models;
using SearchScopeAPI.SearchScope.Infrastructure.Data;

namespace SearchScopeAPI.SearchScope.Infrastructure.Repositories
{
    public class SearchHistoryRepository : ISearchHistoryRepository
    {
        private readonly SearchScopeDbContext _context;

        public SearchHistoryRepository(SearchScopeDbContext context)
        {
            _context = context;
        }

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
