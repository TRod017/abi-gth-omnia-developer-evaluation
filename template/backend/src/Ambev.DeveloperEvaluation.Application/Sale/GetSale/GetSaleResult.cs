using Ambev.DeveloperEvaluation.Application.Sales.GetSale.SaleItems;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Represents the response model returned after successfully retrieving a Sale by its ID.
/// </summary>
/// <remarks>
/// This DTO contains detailed Sale information retrieved from the database,
/// including Sale ID, associated user ID, status, creation and update timestamps,
/// as well as the list of items in the Sale.
/// </remarks>
public class GetSaleResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the Sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who owns the Sale.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the current status of the Sale.
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the timestamp when the Sale was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the last update to the Sale, if available.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the list of items contained in the Sale.
    /// </summary>
    public List<GetSaleItemResult> Items { get; set; } = new();
}
