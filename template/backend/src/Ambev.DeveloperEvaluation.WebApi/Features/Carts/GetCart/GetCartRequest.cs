namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

/// <summary>
/// Represents the request to retrieve a cart by ID.
/// </summary>
public class GetCartRequest
{
    public Guid Id { get; set; }
}
