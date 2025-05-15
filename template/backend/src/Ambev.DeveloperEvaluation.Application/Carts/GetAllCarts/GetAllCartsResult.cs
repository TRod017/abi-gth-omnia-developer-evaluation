namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;

/// <summary>
/// Represents the result of a cart item returned in the list of all carts.
/// </summary>
public class GetAllCartsResult
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
