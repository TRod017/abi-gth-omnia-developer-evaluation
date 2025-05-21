using Ambev.DeveloperEvaluation.Application.Sales.CreateSale.SaleItem;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Represents the response returned after successfully creating a new sale.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created sale,
/// financial totals, cancellation status, and the list of items sold.
/// </remarks>
public class CreateSaleResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the generated sale number (e.g., VEN-0001).
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unique identifier of the user who made the purchase.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the branch (filial) where the sale occurred.
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total value of the sale before applying discounts.
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// Gets or sets the total value of the sale after applying discounts.
    /// </summary>
    public decimal TotalWithDiscount { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the sale has been cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets the date the sale was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the list of items included in the sale.
    /// </summary>
    public List<CreateSaleItemResult> Items { get; set; } = new();
}
