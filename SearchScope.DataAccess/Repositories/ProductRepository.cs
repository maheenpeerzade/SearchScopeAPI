using SearchScopeAPI.SearchScope.Core.Interfaces;
using SearchScopeAPI.SearchScope.Core.Models;
using SearchScopeAPI.SearchScope.DataAccess.Data;

namespace SearchScopeAPI.SearchScope.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SearchScopeDbContext _context;

        public ProductRepository(SearchScopeDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Product>> GetProductsAsync()
        {
            return await Task.FromResult(_context.Products.AsQueryable());
        }
    }
}
