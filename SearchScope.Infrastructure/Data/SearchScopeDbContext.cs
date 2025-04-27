using Microsoft.EntityFrameworkCore;
using SearchScopeAPI.SearchScope.Core.Models;

namespace SearchScopeAPI.SearchScope.Infrastructure.Data
{
    public class SearchScopeDbContext : DbContext
    {
        public SearchScopeDbContext() { }

        public SearchScopeDbContext(DbContextOptions<SearchScopeDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<SearchHistory> SearchHistories { get; set; }

        public DbSet<SearchResult> SearchResults { get; set; }
    }
}
