using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents an individual item within a finalized sale.
/// </summary>
/// <remarks>
/// Each sale item is linked to a specific product and stores its snapshot details,
/// such as price and name, at the time the sale was confirmed.
/// </remarks>
public class SaleItem : BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the associated sale.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the product being sold.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the name of the product at the time it was sold.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unit price of the product at the time it was sold.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product sold.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets the total value for this sale item (UnitPrice × Quantity).
    /// </summary>
    public decimal Total => UnitPrice * Quantity;

    /// <summary>
    /// Gets the discount percentage applied to the item based on quantity.
    /// </summary>
    public decimal DiscountPercentage
    {
        get
        {
            if (Quantity >= 10 && Quantity <= 20) return 0.20m;
            if (Quantity >= 4 && Quantity < 10) return 0.10m;
            return 0.00m;
        }
    }

    /// <summary>
    /// Gets the discount value applied to the item.
    /// </summary>
    public decimal Discount => Total * DiscountPercentage;

    /// <summary>
    /// Gets the total value after applying discount.
    /// </summary>
    public decimal TotalWithDiscount => Total - Discount;

    /// <summary>
    /// Gets or sets the date and time the sale item was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time the sale item was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SaleItem"/> class.
    /// </summary>
    public SaleItem()
    {
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Validates the current sale item instance using the <see cref="SaleItemValidator"/>.
    /// </summary>
    /// <returns>A <see cref="ValidationResultDetail"/> with validation status and errors.</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new SaleItemValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
        };
    }

    /// <summary>
    /// Ensures business rules specific to this sale item are respected.
    /// Throws <see cref="DomainException"/> if any rule is violated.
    /// </summary>
    public void EnsureBusinessRulesAreMet()
    {
        EnsureValidQuantity();

        if (UnitPrice <= 0)
        {
            throw new DomainException($"Product '{ProductName}' must have a valid positive price.");
        }

        if (Quantity <= 0)
        {
            throw new DomainException($"Product '{ProductName}' must have a positive quantity.");
        }

        if (Quantity < 4 && Discount > 0)
        {
            throw new DomainException($"Product '{ProductName}' has a discount, but quantity is below 4.");
        }
    }

    /// <summary>
    /// Ensures that the quantity of this item does not exceed the allowed maximum.
    /// Throws <see cref="DomainException"/> if the quantity is invalid.
    /// </summary>
    public void EnsureValidQuantity()
    {
        if (Quantity > 20)
        {
            throw new DomainException($"Product '{ProductName}' exceeds the limit of 20 units per sale.");
        }
    }
}
