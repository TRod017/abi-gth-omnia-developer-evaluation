namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// Request for retrieving a product by ID.
/// </summary>
public class GetProductRequest
{
    public Guid Id { get; set; }
}
