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
    /// Gets or sets the user who owns this cart.
    /// This navigation property defines the relationship between the cart and its associated user.
    /// </summary>
    public User User { get; set; } = null!;


    /// <summary>
    /// Initializes a new instance of the <see cref="Cart"/> class.
    /// </summary>
    public Cart()
    {
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Gets the total amount for the cart before applying discounts.
    /// </summary>
    public decimal Total => Items.Sum(i => i.Total);

    /// <summary>
    /// Gets the total amount for the cart after applying discounts to each item.
    /// </summary>
    public decimal TotalWithDiscount => Items.Sum(i => i.TotalWithDiscount);

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
            var result = item.Validate();

            if (!result.IsValid)
            {
                errors.AddRange(result.Errors);
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

    /// <summary>
    /// Ensures that the quantity of a given item does not exceed business limits.
    /// Throws <see cref="DomainException"/> if the quantity is invalid.
    /// </summary>
    /// <param name="item">The item to validate.</param>
    public void EnsureValidQuantity(CartItem item)
    {
        item.EnsureValidQuantity();
    }

    /// <summary>
    /// Ensures all business rules related to cart items are respected.
    /// Throws <see cref="DomainException"/> when any rule is violated.
    /// </summary>
    public void EnsureBusinessRulesAreMet()
    {
        foreach (var item in Items)
        {
            item.EnsureBusinessRulesAreMet();
        }
    }
}
