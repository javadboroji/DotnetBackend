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
    public async Task<ActionResult<List<ProductResponseDto>>> GetAll()
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductResponseDto>> GetById(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product is null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequestDto request)
    {
        var productId = await _productService.CreateAsync(request);
        return CreatedAtAction(
            nameof(GetById),
            new { id = productId },
            new { id = productId }
        );
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductRequestDto request)
    {
        var updated = await _productService.UpdateAsync(id, request);
        if (!updated)
            return NotFound();

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        var deactivated = await _productService.DeactivateAsync(id);
        if (!deactivated)
            return NotFound();

        return Ok();
    }
    [HttpPost("{id:guid}/activate")]
    public async Task<IActionResult> Activate(Guid id)
    {
        var activated = await _productService.ActivateAsync(id);
        if (!activated)
            return NotFound();

        return Ok();
    }
}


