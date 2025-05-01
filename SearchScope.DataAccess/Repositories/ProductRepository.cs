using SearchScopeAPI.SearchScope.Core.Interfaces;
using SearchScopeAPI.SearchScope.Core.Models;
using SearchScopeAPI.SearchScope.DataAccess.Data;

namespace SearchScopeAPI.SearchScope.DataAccess.Repositories
{
    /// <summary>
    /// ProductRepository class.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly SearchScopeDbContext _context;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">Specify SearchScopeDbContext.</param>
        public ProductRepository(SearchScopeDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// To get all product.
        /// </summary>
        /// <returns>Products.</returns>
        public async Task<IQueryable<Product>> GetProductsAsync()
        {
            return await Task.FromResult(_context.Products.AsQueryable());
        }
    }
}
