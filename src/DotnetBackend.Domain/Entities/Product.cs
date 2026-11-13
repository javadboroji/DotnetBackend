namespace DotnetBackend.Domain.Entities;

public class Product
{

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Product name cannot be null or empty.",
                nameof(name));
    }
    private static void ValidatePrice(decimal price)
    {
        if (price <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(price),
                "Price must be greater than zero.");
    }

    private static void ValidateCategoryId(Guid categoryId)
    {
        if (categoryId == Guid.Empty)
            throw new ArgumentException("CategoryId cannot be empty.", nameof(categoryId));
    }
    public Product(
           string name,
            decimal price,
            Guid categoryId,
            string? description = null
    )
    {
        ValidateName(name);
        Name = name.Trim();
        ValidatePrice(price);
        Price = price;
        ValidateCategoryId(categoryId);
        CategoryId = categoryId;
        Description = string.IsNullOrWhiteSpace(description)
       ? null
       : description.Trim();
    }
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }

    public string? Description { get; private set; }

    public decimal Price { get; private set; }

    public bool IsActive { get; private set; } = true;


    public void Deactivate()
    {
        IsActive = false;
    }
    public void Activate()
    {
        IsActive = true;
    }
    public Guid CategoryId { get; private set; }

    public Category Category { get; private set; } = null!;

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; private set; }


    public void ChangeCategory(Guid categoryId)
    {
        ValidateCategoryId(categoryId);

        CategoryId = categoryId;
        UpdatedAt = DateTime.UtcNow;
    }
    public void Update(
          string name,
            decimal price,
            string? description = null
    )
    {
        ValidateName(name);
        ValidatePrice(price);
        Name = name.Trim();
        Price = price;
        Description = string.IsNullOrWhiteSpace(description)
       ? null
       : description.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

}