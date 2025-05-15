namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Represents the response returned by the API after successfully creating a product.
/// </summary>
/// <remarks>
/// Contains the unique identifier of the newly created product,
/// typically used to confirm the operation or retrieve the product in future requests.
/// </remarks>
public class CreateProductResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created product.
    /// </summary>
    public Guid Id { get; set; }
}
