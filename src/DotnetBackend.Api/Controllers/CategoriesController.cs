
using DotnetBackend.Application.Categories;
using DotnetBackend.Application.Categories.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DotnetBackend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoriesController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateCategoryRequestDto request)
    {
        var categoryId = await _categoryService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = categoryId },
            new { id = categoryId }
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await _categoryService.GetByIdAsync(id);

        if (category is null)
            return NotFound();

        return Ok(category);
    }
}
