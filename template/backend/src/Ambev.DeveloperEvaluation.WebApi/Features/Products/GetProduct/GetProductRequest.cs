namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// Represents the request model used to retrieve a product by its unique identifier via the API.
/// </summary>
/// <remarks>
/// Contains the <see cref="Id"/> of the product to be fetched.
/// </remarks>
public class GetProductRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the product to retrieve.
    /// </summary>
    public Guid Id { get; set; }
}
