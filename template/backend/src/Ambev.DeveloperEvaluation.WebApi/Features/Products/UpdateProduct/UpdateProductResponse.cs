namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

/// <summary>
/// Represents the response returned by the API after successfully updating a product.
/// </summary>
/// <remarks>
/// Contains the unique identifier of the updated product, allowing clients
/// to confirm the update or use the ID for subsequent operations.
/// </remarks>
public class UpdateProductResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the updated product.
    /// </summary>
    public Guid Id { get; set; }
}
