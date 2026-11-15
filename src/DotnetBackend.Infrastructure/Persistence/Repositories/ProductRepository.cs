
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
        return await _context.Products
        .Where(product => product.IsActive)
         .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
    }

    public async Task<Product?> GetByIdIncludingInactiveAsync(Guid id)
    {
        return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);
    }
    
    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();


    }

}