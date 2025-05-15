namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;

/// <summary>
/// Request for deleting a product by ID.
/// </summary>
public class DeleteProductRequest
{
    public Guid Id { get; set; }
}
