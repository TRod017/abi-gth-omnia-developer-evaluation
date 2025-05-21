namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;

/// <summary>
/// Represents the request payload for deleting a product by its unique identifier.
/// </summary>
/// <remarks>
/// This request is typically sent via the API to trigger the deletion of a product
/// based on the provided <see cref="Id"/>.
/// </remarks>
public class DeleteProductRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the product to be deleted.
    /// </summary>
    public Guid Id { get; set; }
}
