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
}