namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Represents the response returned by the API after successfully creating a Sale.
/// </summary>
/// <remarks>
/// Contains all relevant information about the newly created Sale,
/// including metadata, customer, branch, financial summary and items.
/// </remarks>
public class CreateSaleResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created Sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the generated sale number (e.g., VEN-0001).
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unique identifier of the customer (user).
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the branch (filial) where the sale was made.
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date and time the sale was registered.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the total value before discount.
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// Gets or sets the total value after applying discounts.
    /// </summary>
    public decimal TotalWithDiscount { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the sale has been cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets the list of items included in the sale.
    /// </summary>
    public List<CreateSaleItemResponse> Items { get; set; } = new();
}
