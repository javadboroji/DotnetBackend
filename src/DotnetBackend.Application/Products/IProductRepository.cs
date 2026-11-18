using DotnetBackend.Application.Common.Pagination;
using DotnetBackend.Application.Common.Pagnation;
using DotnetBackend.Domain.Entities;

namespace DotnetBackend.Application.Products;

public interface IProductsRepository
{
    Task<PagedResult<Product>> GetAllProductAsync(PageRequest request ,ProductFilterRequest filter);

    Task<Product?> GetByIdAsync(Guid id);

    Task<Product?> GetByIdIncludingInactiveAsync(Guid id);

    Task AddAsync(Product product);

    Task UpdateAsync(Product product);
}