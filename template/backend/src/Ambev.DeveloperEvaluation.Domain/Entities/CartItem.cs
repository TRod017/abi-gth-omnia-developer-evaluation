using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents an individual item within a shopping cart.
/// </summary>
/// <remarks>
/// Each cart item is linked to a specific product and stores its snapshot details,
/// such as price and name, at the time it was added to the cart.
/// </remarks>
public class CartItem : BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the associated cart.
    /// </summary>
    public Guid CartId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the product being purchased.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the name of the product at the time it was added to the cart.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unit price of the product at the time it was added to the cart.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product added to the cart.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets the total value for this cart item (UnitPrice × Quantity).
    /// </summary>
    public decimal Total => UnitPrice * Quantity;

    /// <summary>
    /// Gets or sets the date and time the cart item was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time the cart item was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CartItem"/> class.
    /// </summary>
    public CartItem()
    {
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Validates the current cart item instance using the <see cref="CartItemValidator"/>.
    /// </summary>
    /// <returns>A <see cref="ValidationResultDetail"/> with validation status and errors.</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new CartItemValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
        };
    }
}
