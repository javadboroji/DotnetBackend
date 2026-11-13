using DotnetBackend.Application.Categories;
using DotnetBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotnetBackend.Infrastructure.Persistence.Repositories;

public class CategoryRepository : ICategoriesRepository
{
    private readonly AppDbContext _context;
    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task AddAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Categories
            .AnyAsync(category => category.Id == id);
    }

}