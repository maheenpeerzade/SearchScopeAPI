using MediatR;
using Microsoft.EntityFrameworkCore;
using SearchScopeAPI.SearchScope.Core.Interfaces;
using SearchScopeAPI.SearchScope.Core.Models;
using SearchScopeAPI.SearchScope.Core.Queries;
using SearchScopeAPI.SearchScope.Core.Utility;
using SearchScopeAPI.SerachScope.API.Logger;

namespace SearchScopeAPI.SearchScope.Application.Handlers
{
    /// <summary>
    /// SearchProductsQueryHandler class.
    /// </summary>
    public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _productRepository;
        private readonly CustomLogger _customLogger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="productRepository">Specify product repository.</param>
        /// <param name="customLogger">Specify custom logger.</param>
        public SearchProductsQueryHandler(IProductRepository productRepository, CustomLogger customLogger)
        {
            _productRepository = productRepository;
            _customLogger = customLogger;
        }

        /// <summary>
        /// To handle search products request.
        /// </summary>
        /// <param name="request">Specify search product query.</param>
        /// <param name="cancellationToken">Specify cancellation token.</param>
        /// <returns>Products.</returns>
        public async Task<IEnumerable<Product>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve products from the repository
                var products = await _productRepository.GetProductsAsync();

                // Apply query filtering
                products = ApplyQuery(request.Query, products);

                // Apply additional filtering
                products = ApplyFilter(request.Filter, products);

                // Apply sorting
                products = ApplySorting(request.SortBy, request.IsAscending, products);


                // Apply pagination
                var paginatedProducts = products
                   .Skip((request.PageNumber - 1) * request.PageSize)
                     .Take(request.PageSize);

                return await paginatedProducts.ToListAsync();
            }
            catch (Exception ex)
            {
                _customLogger.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// To apply query.
        /// </summary>
        /// <param name="query">Specify query.</param>
        /// <param name="products">Specify products.</param>
        /// <returns>Products.</returns>
        private static IQueryable<Product> ApplyQuery(string? query, IQueryable<Product> products)
        {
            if (!string.IsNullOrEmpty(query))
            {
                products = products.Where(p => (p.Id.ToString().Contains(query)) ||
                                         (p.Name != null && p.Name.Contains(query)) ||
                                         (p.Description != null && p.Description.Contains(query)));
            }
            return products;
        }

        /// <summary>
        /// To apply filter.
        /// </summary>
        /// <param name="filter">Specify filter.</param>
        /// <param name="products">Specify products.</param>
        /// <returns>Products.</returns>
        private static IQueryable<Product> ApplyFilter(string? filter, IQueryable<Product> products)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                var filterParts = filter.Split(':');
                if (filterParts.Length == 2)
                {
                    var columnName = filterParts[0].ToLower();
                    var filterValue = filterParts[1];

                    switch (columnName)
                    {
                        case "discontinued":
                            if (bool.TryParse(filterValue, out var isDiscontinued))
                            {
                                products = products.Where(p => p.Discontinued == isDiscontinued);
                            }
                            break;

                        case "category":
                            products = products.Where(p => p.Category != null && p.Category.Contains(filterValue));
                            break;
                    }
                }
            }
            return products;
        }

        /// <summary>
        /// To apply sorting.
        /// </summary>
        /// <param name="sort">Specify sort.</param>
        /// <param name="isAscending">Specify isAscending.</param>
        /// <param name="products">Specify products.</param>
        /// <returns>Products.</returns>
        private static IQueryable<Product> ApplySorting(ProductEnum? sort, bool isAscending, IQueryable<Product> products)
        {
            if (sort.HasValue)
            {
                switch (sort.Value)
                {
                    case ProductEnum.UnitPrice:
                        products = isAscending ? products.OrderBy(p => p.UnitPrice) : products.OrderByDescending(p => p.UnitPrice);
                        break;

                    case ProductEnum.UnitsInStock:
                        products = isAscending ? products.OrderBy(p => p.UnitsInStock) : products.OrderByDescending(p => p.UnitsInStock);
                        break;

                    case ProductEnum.Popularity:
                        products = isAscending ? products.OrderBy(p => p.Popularity) : products.OrderByDescending(p => p.Popularity);
                        break;

                    case ProductEnum.ProductionData:
                        products = isAscending ? products.OrderBy(p => p.ProductionData) : products.OrderByDescending(p => p.ProductionData);
                        break;

                    default: // Fallback sorting by Id
                        products = isAscending ? products.OrderBy(p => p.Id) : products.OrderByDescending(p => p.Id);
                        break;
                }
            }
            return products;
        }
    }
}
