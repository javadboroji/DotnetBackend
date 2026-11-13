using System.ComponentModel.DataAnnotations;

namespace DotnetBackend.Application.Categories.Dtos;
public class CreateCategoryRequestDto
{
    [Required(ErrorMessage = " Name is Required")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? description{ get; set; }
}