
using DotnetBackend.Application.Products;
using DotnetBackend.Domain.Entities;
using DotnetBackend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DotnetBackend.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductsRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllProductAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

}