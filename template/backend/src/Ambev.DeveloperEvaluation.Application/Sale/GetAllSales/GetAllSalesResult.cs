using Ambev.DeveloperEvaluation.Application.Sales.CreateSale.SaleItem;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

/// <summary>
/// Represents the response model returned for each Sale in the GetAllSales operation.
/// </summary>
/// <remarks>
/// This DTO is used to expose selected Sale fields in Sale listing endpoints,
/// including the Sale ID, associated user, status, and creation/update timestamps.
/// </remarks>
public class GetAllSalesResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the Sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user associated with the Sale.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the Sale was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the Sale was last updated, if applicable.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the branch (filial) where the Sale was made.
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unique sale number.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total amount before discount.
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// Gets or sets the total amount after applying discounts.
    /// </summary>
    public decimal TotalWithDiscount { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the sale is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the Cart used to generate this Sale.
    /// </summary>
    public Guid CartId { get; set; }

    /// <summary>
    /// Gets or sets the list of items included in the Sale.
    /// </summary>
    public List<CreateSaleItemResult> Items { get; set; } = new();
}
