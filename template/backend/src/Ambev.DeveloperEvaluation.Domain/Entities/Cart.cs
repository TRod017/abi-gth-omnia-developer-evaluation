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
    /// Gets the total amount for the cart after applying discounts to each item.
    /// </summary>
    public decimal Total => Items.Sum(i => i.TotalWithDiscount);

    /// <summary>
    /// Checks whether the quantity of a specific item is within the allowed limit.
    /// </summary>
    /// <param name="item">The cart item to validate.</param>
    /// <returns>True if quantity is less than or equal to 20, false otherwise.</returns>
    public bool IsValidQuantity(CartItem item)
    {
        return item.Quantity <= 20;
    }

    /// <summary>
    /// Validates business rules specific to the cart, such as quantity limits and discount eligibility.
    /// </summary>
    /// <returns>A <see cref="ValidationResultDetail"/> containing any rule violations.</returns>
    public ValidationResultDetail ValidateBusinessRules()
    {
        var errors = new List<ValidationErrorDetail>();

        foreach (var item in Items)
        {
            if (item.Quantity > 20)
            {
                errors.Add(new ValidationErrorDetail
                {
                    Error = "QuantityLimitExceeded",
                    Detail = $"O produto '{item.ProductName}' excede o limite de 20 unidades permitidas."
                });
            }

            if (item.Quantity < 4 && item.Discount > 0)
            {
                errors.Add(new ValidationErrorDetail
                {
                    Error = "InvalidDiscount",
                    Detail = $"O produto '{item.ProductName}' está com desconto, mas a quantidade é inferior a 4."
                });
            }
        }

        return new ValidationResultDetail
        {
            IsValid = errors.Count == 0,
            Errors = errors
        };
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
