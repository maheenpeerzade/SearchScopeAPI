using MediatR;
using Microsoft.EntityFrameworkCore;
using SearchScopeAPI.SearchScope.Core.Interface;
using SearchScopeAPI.SearchScope.Core.Models;
using SearchScopeAPI.SearchScope.Core.Queries;
using SearchScopeAPI.SearchScope.Core.Utility;

namespace SearchScopeAPI.SearchScope.Application.Handlers
{
    public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _productRepository;

        public SearchProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

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

                // Execute the query
                return await products.ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

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
