using SearchScopeAPI.SearchScope.Core.Models;

namespace SearchScopeAPI.SearchScope.Core.Interfaces
{
    /// <summary>
    /// IProductRepository interface.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// To get all product.
        /// </summary>
        /// <returns>Products.</returns>
        Task<IQueryable<Product>> GetProductsAsync();
    }
}
