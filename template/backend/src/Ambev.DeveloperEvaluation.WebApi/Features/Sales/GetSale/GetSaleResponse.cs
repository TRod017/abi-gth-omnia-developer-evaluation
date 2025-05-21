namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// API response model for retrieving a single Sale by its unique identifier.
/// </summary>
/// <remarks>
/// Includes full Sale information, such as user ID, status, timestamps,
/// branch, cart ID, financial totals, and a list of Sale items.
/// </remarks>
public class GetSaleResponse
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
    /// Gets or sets the timestamp indicating when the Sale was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the timestamp indicating when the Sale was last updated, if applicable.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the name of the branch where the Sale occurred.
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sale number for identification.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total amount before discount.
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// Gets or sets the total amount after discount.
    /// </summary>
    public decimal TotalWithDiscount { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the Sale was cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the original Cart used in this Sale.
    /// </summary>
    public Guid CartId { get; set; }

    /// <summary>
    /// Gets or sets the list of items contained within the Sale.
    /// </summary>
    public List<GetSaleItemResponse> Items { get; set; } = new();
}
