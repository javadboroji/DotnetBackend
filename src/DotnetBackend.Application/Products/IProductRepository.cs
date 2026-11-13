using DotnetBackend.Domain.Entities;

namespace DotnetBackend.Application.Products;

public interface IProductsRepository
{
    Task<List<Product>> GetAllProductAsync();

    Task<Product?> GetByIdAsync(Guid id);

    Task AddAsync(Product product);
}