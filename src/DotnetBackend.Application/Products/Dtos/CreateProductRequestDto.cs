using System.ComponentModel.DataAnnotations;

namespace DotnetBackend.Application.Products.Dtos;

public class CreateProductRequestDto
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Description { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    public Guid CategoryId { get; set; }
}