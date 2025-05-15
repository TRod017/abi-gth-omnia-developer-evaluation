namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Represents the response returned after successfully updating a product.
/// </summary>
public class UpdateProductResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the updated product.
    /// </summary>
    public Guid Id { get; set; }
}
