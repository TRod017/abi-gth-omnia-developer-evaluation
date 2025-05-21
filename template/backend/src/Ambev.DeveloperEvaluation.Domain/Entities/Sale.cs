using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a finalized sale derived from a cart, containing sale items and financial totals.
/// </summary>
/// <remarks>
/// The sale entity stores information about the user, original cart, discount totals, and status of the transaction.
/// </remarks>
public class Sale : BaseEntity
{
    /// <summary>
    /// Gets or sets the ID of the cart that originated this sale.
    /// </summary>
    public Guid CartId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user who made the purchase.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of when the cart was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the last update to the cart.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the collection of items included in this sale.
    /// </summary>
    public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();

    /// <summary>
    /// Gets or sets the sale number for identification purposes.
    /// Typically a unique business identifier such as "VEN-0001".
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total value of the sale before applying discounts.
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// Gets or sets the total value of the sale after applying discounts.
    /// </summary>
    public decimal TotalWithDiscount { get; set; }

    /// <summary>
    /// Gets or sets the name of the branch where the sale occurred.
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the sale has been cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Ensures all business rules related to sale items are respected.
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
