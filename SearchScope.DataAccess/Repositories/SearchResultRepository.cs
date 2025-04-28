using Microsoft.EntityFrameworkCore;
using SearchScopeAPI.SearchScope.Core.Interfaces;
using SearchScopeAPI.SearchScope.Core.Models;
using SearchScopeAPI.SearchScope.DataAccess.Data;

namespace SearchScopeAPI.SearchScope.DataAccess.Repositories
{
    public class SearchResultRepository : ISearchResultRepository
    {
        private readonly SearchScopeDbContext _context;

        public SearchResultRepository(SearchScopeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SearchResult>> GetSearchResultsAsync(int searchHistoryId)
        {
            return await _context.SearchResults.Where(sr => sr.SearchHistoryId == searchHistoryId).ToListAsync();
        }
    }
}
