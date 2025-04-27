using SearchScopeAPI.SearchScope.Core.Models;

namespace SearchScopeAPI.SearchScope.Core.Interface
{
    public interface IProductRepository
    {
        Task<IQueryable<Product>> GetProductsAsync();
    }
}
