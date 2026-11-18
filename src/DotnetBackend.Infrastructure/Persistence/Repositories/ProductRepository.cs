
using DotnetBackend.Application.Common.Pagination;
using DotnetBackend.Application.Common.Pagnation;
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

    public async Task<PagedResult<Product>> GetAllProductAsync(PageRequest request, ProductFilterRequest filter)
    {
        var query = _context.Products
       .Where(p => p.IsActive);

        if (!string.IsNullOrWhiteSpace(filter.name))
            query = query.Where(p => p.Name.Contains(filter.name));

        if (filter.MinPrice.HasValue)
            query = query.Where(p => p.Price >= filter.MinPrice.Value);

        if (filter.MaxPrice.HasValue)
            query = query.Where(p => p.Price <= filter.MaxPrice.Value);

        if (filter.categoryId.HasValue)
            query = query.Where(p => p.CategoryId == filter.categoryId.Value);

        var totalCount = await query.CountAsync();

        query = request.SortBy?.ToLower() switch
        {
            "name" => request.SortDescending
                ? query.OrderByDescending(p => p.Name)
                : query.OrderBy(p => p.Name),

            "price" => request.SortDescending
                ? query.OrderByDescending(p => p.Price)
                : query.OrderBy(p => p.Price),

            "createdat" => request.SortDescending
                ? query.OrderByDescending(p => p.CreatedAt)
                : query.OrderBy(p => p.CreatedAt),

            _ => query.OrderByDescending(p => p.CreatedAt)
        };

        var items = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new PagedResult<Product>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        };
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