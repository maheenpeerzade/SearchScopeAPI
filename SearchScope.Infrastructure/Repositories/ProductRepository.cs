using SearchScopeAPI.SearchScope.Core.Interface;
using SearchScopeAPI.SearchScope.Core.Models;
using SearchScopeAPI.SearchScope.Infrastructure.Data;

namespace SearchScopeAPI.SearchScope.Infrastructure.Repositories
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
