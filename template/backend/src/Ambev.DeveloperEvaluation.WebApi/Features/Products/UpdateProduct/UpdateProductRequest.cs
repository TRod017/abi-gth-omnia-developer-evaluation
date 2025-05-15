namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

/// <summary>
/// Represents the request payload used to update an existing product via the API.
/// </summary>
/// <remarks>
/// Contains the updated product information, including its identifier, name,
/// description, unit price, and available quantity.
/// </remarks>
public class UpdateProductRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the product to be updated.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the updated name of the product.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the updated description of the product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the updated unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the updated available quantity of the product.
    /// </summary>
    public int AvailableQuantity { get; set; }
}
