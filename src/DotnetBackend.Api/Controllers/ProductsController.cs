using DotnetBackend.Api.Common.Responses;
using DotnetBackend.Application.Common.Pagination;
using DotnetBackend.Application.Common.Pagnation;
using DotnetBackend.Application.Products;
using DotnetBackend.Application.Products.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DotnetBackend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }
    [HttpGet]
    public async Task<ActionResult<PagedResult<ProductResponseDto>>> GetAll([FromQuery] PageRequest request)
    {
        var products = await _productService.GetAllAsync(request);
        return Ok(
            ApiResponse<PagedResult<ProductResponseDto>>.Ok(products, "Products retrieved successfully.")
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductResponseDto>> GetById(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);

        return Ok(
            ApiResponse<ProductResponseDto>.Ok(product, "Product retrieved successfully.")
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequestDto request)
    {
        var productId = await _productService.CreateAsync(request);
        return CreatedAtAction(
            nameof(GetById),
            new { id = productId },
             ApiResponse<object>.Ok(
            new { id = productId },
            "Product created successfully."
        )
        );
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductRequestDto request)
    {
        var updated = await _productService.UpdateAsync(id, request);
        if (!updated)
            return NotFound(
                ApiResponse<object>.Fail("Product not found.")
            );

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        var deactivated = await _productService.DeactivateAsync(id);
        if (!deactivated)
            return NotFound(
                ApiResponse<object>.Fail("Product not found.")
            );

        return Ok(
            ApiResponse<object>.Ok(null, "Product deactivated successfully.")
        );
    }
    [HttpPost("{id:guid}/activate")]
    public async Task<IActionResult> Activate(Guid id)
    {
        var activated = await _productService.ActivateAsync(id);
        if (!activated)
            return NotFound(
                ApiResponse<object>.Fail("Product not found.")
            );

        return Ok(
            ApiResponse<object>.Ok(null, "Product activated successfully.")
        );
    }
}


