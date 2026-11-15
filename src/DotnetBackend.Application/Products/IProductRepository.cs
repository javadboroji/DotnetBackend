using DotnetBackend.Domain.Entities;

namespace DotnetBackend.Application.Products;

public interface IProductsRepository
{
    Task<List<Product>> GetAllProductAsync();

    Task<Product?> GetByIdAsync(Guid id);

    Task<Product?> GetByIdIncludingInactiveAsync(Guid id);

    Task AddAsync(Product product);

    Task UpdateAsync(Product product);
}