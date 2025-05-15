namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// Represents the response model returned by the API when retrieving a product by ID.
/// </summary>
/// <remarks>
/// Contains detailed product information such as its identifier, name, description,
/// unit price, and available quantity in stock.
/// </remarks>
public class GetProductResponse
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
    /// Gets or sets the available quantity of the product in stock.
    /// </summary>
    public int AvailableQuantity { get; set; }
}
