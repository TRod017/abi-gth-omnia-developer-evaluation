using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;

/// <summary>
/// Represents the result of a cart item returned in the list of all carts.
/// </summary>
public class GetAllCartsResult
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public CartStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
