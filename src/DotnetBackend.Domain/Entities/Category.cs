namespace DotnetBackend.Domain.Entities;

public class Category
{
    public Category(string name, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Category name cannot be null or empty.", nameof(name));
        }
        Name = name.Trim();

        Description = string.IsNullOrWhiteSpace(description)
        ? null
        : description.Trim();

    }
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }

    public string? Description { get; private set; }

    public bool IsActive { get; private set; } = true;

    public void Deactivate()
    {
        IsActive = false;
    }
    public void Activate()
    {
        IsActive = true;
    }

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public void Update(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be null or empty.", nameof(name));

        Name = name.Trim();

        Description = string.IsNullOrWhiteSpace(description)
            ? null
            : description.Trim();

        UpdatedAt = DateTime.UtcNow;
    }
    public DateTime? UpdatedAt { get; private set; }

    public ICollection<Product> Products { get; private set; }
    = new List<Product>();
}