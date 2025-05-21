namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Represents the response model returned after successfully cancelling a Sale.
/// </summary>
/// <remarks>
/// This DTO contains summary information about the cancelled Sale,
/// including its ID, cancellation status, and key financial fields.
/// </remarks>
public class CancelSaleResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the cancelled Sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who made the purchase.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the Cart associated with the Sale.
    /// </summary>
    public Guid CartId { get; set; }

    /// <summary>
    /// Gets or sets the sale number.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch where the Sale occurred.
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the timestamp of when the Sale was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the last update to the Sale, if any.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the total value before discounts.
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// Gets or sets the total value after discounts.
    /// </summary>
    public decimal TotalWithDiscount { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating whether the Sale has been cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
