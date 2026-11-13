

using DotnetBackend.Application.Categories.Dtos;
using DotnetBackend.Domain.Entities;

namespace DotnetBackend.Application.Categories;

public class CategoryService
{
    private readonly ICategoriesRepository _categoryRepository;
    public CategoryService(ICategoriesRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Guid> CreateAsync(CreateCategoryRequestDto request)
    {
        var category = new Category(
            request.Name,
            request.description
        );

        await _categoryRepository.AddAsync(category);

        return category.Id;


    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await _categoryRepository.GetByIdAsync(id);
    }
}