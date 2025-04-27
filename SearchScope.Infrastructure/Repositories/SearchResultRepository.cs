using Microsoft.EntityFrameworkCore;
using SearchScopeAPI.SearchScope.Core.Interface;
using SearchScopeAPI.SearchScope.Core.Models;
using SearchScopeAPI.SearchScope.Infrastructure.Data;

namespace SearchScopeAPI.SearchScope.Infrastructure.Repositories
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
