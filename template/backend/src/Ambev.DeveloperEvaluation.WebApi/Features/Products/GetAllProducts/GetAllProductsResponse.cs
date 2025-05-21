namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts;

/// <summary>
/// Represents the response model returned by the API when retrieving all products.
/// </summary>
/// <remarks>
/// Contains summarized information for each product, including identification,
/// name, description, price, and available stock quantity.
/// </remarks>
public class GetAllProductsResponse
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
