using DotnetBackend.Application.Products;
using DotnetBackend.Domain.Entities;
using DotnetBackend.Application.Products.Dtos;
using DotnetBackend.Application.Categories;

namespace DotnetBackend.Application.Products;

public class ProductService
{
    private readonly IProductsRepository _ProductsRepository;

    private readonly ICategoriesRepository _CategoryRepository;

    public ProductService(IProductsRepository ProductsRepository, ICategoriesRepository CategoryRepository)
    {
        _ProductsRepository = ProductsRepository;
        _CategoryRepository = CategoryRepository;
    }

    public async Task<List<ProductResponseDto>> GetAllAsync()
    {
        var products = await _ProductsRepository.GetAllProductAsync();

        return products.Select(product => new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId
        }
        ).ToList();
    }

    public async Task<ProductResponseDto?> GetByIdAsync(Guid id)
    {
        var product = await _ProductsRepository.GetByIdAsync(id);

        if (product is null)
            return null;

        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId
        };
    }

    public async Task<Guid> CreateAsync(CreateProductRequestDto request)
    {
        var category = await _CategoryRepository.GetByIdAsync(request.CategoryId);
        if (category is null)
            throw new ArgumentException("Category not found.");

        if (!category.IsActive)
            throw new InvalidOperationException("Category is inactive.");

        var product = new Product(
            request.Name,
            request.Price,
            request.CategoryId,
            request.Description
        );

        await _ProductsRepository.AddAsync(product);

        return product.Id;
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateProductRequestDto request)
    {
        var product = await _ProductsRepository.GetByIdAsync(id);

        if (product is null)
            return false;

        var category = await _CategoryRepository.GetByIdAsync(request.CategoryId);

        if (category is null)
            throw new ArgumentException("Category not found.");

        if (!category.IsActive)
            throw new InvalidOperationException("Category is inactive.");

        product.Update(request.Name, request.Price, request.Description);

        product.ChangeCategory(request.CategoryId);


        await _ProductsRepository.UpdateAsync(product);
        return true;
    }

    public async Task<bool> DeactivateAsync(Guid id)
    {
        var product = await _ProductsRepository.GetByIdAsync(id);

        if (product is null)
            return false;

        product.Deactivate();

        await _ProductsRepository.UpdateAsync(product);

        return true;
    }

    public async Task<bool> ActivateAsync(Guid id)
    {
        var product = await _ProductsRepository.GetByIdIncludingInactiveAsync(id);

        if (product is null)
            return false;

        product.Activate();

        await _ProductsRepository.UpdateAsync(product);

        return true;
    }
}