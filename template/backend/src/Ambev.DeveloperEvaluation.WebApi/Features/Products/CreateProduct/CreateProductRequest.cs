namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Represents the request payload used to create a new product via the API.
/// </summary>
/// <remarks>
/// Contains the necessary information to register a new product, including name,
/// description, price, and available stock quantity.
/// </remarks>
public class CreateProductRequest
{
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
    /// Gets or sets the available quantity in stock.
    /// </summary>
    public int AvailableQuantity { get; set; }
}
