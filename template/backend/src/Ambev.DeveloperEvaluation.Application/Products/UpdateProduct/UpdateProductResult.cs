namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Represents the response model returned after successfully updating a product.
/// </summary>
/// <remarks>
/// This DTO contains the unique identifier of the updated product, typically used
/// to confirm the update operation and reference the product in subsequent actions.
/// </remarks>
public class UpdateProductResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the updated product.
    /// </summary>
    public Guid Id { get; set; }
}
