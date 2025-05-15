using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a shopping cart associated with a specific user.
/// </summary>
/// <remarks>
/// The cart holds a collection of items and tracks its status within the system.
/// </remarks>
public class Cart : BaseEntity
{
    /// <summary>
    /// Gets or sets the user ID that owns this cart.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the collection of items in this cart.
    /// </summary>
    public ICollection<CartItem> Items { get; set; } = new List<CartItem>();

    /// <summary>
    /// Gets or sets the current status of the cart (e.g., Open, Confirmed).
    /// </summary>
    public CartStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of when the cart was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the last update to the cart.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Cart"/> class.
    /// </summary>
    public Cart()
    {
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Validates the cart entity using <see cref="CartValidator"/>.
    /// </summary>
    /// <returns>A validation result detailing any rule violations.</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new CartValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
        };
    }
}
