namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Represents the response model returned after successfully retrieving a product by its ID.
/// </summary>
/// <remarks>
/// This DTO contains the detailed product information retrieved from the database,
/// including its ID, name, description, unit price, and available quantity in stock.
/// </remarks>
public class GetProductResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the available stock quantity of the product.
    /// </summary>
    public int AvailableQuantity { get; set; }
}
