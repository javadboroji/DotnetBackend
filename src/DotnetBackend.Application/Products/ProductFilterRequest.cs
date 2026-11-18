using System.ComponentModel.DataAnnotations;

namespace DotnetBackend.Application.Products;

public class ProductFilterRequest
{
    public string? name { get; set; }

    public Guid? categoryId { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "MinPrice cannot be negative.")]

    public decimal? MinPrice { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "MaxPrice cannot be negative.")]

    public decimal? MaxPrice { get; set; }

}