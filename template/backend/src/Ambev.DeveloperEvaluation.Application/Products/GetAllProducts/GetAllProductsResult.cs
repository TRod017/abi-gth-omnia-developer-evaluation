namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

/// <summary>
/// Represents the response returned for each product in a product list.
/// </summary>
public class GetAllProductsResult
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
