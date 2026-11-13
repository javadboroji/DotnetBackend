using DotnetBackend.Domain.Entities;

namespace DotnetBackend.Application.Categories;

public interface ICategoriesRepository
{
    Task<List<Category>> GetAllAsync();

    Task<Category?> GetByIdAsync(Guid id);

    Task AddAsync(Category category);

    Task<bool> ExistsAsync(Guid id);
}