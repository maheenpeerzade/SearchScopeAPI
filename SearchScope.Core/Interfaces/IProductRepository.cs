using SearchScopeAPI.SearchScope.Core.Models;

namespace SearchScopeAPI.SearchScope.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IQueryable<Product>> GetProductsAsync();
    }
}
