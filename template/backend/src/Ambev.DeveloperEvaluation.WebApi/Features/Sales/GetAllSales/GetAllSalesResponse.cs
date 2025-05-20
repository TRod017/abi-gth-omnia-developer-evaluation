namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales;

/// <summary>
/// Represents a summary view of a Sale in a list returned by the GetAllSales endpoint.
/// </summary>
/// <remarks>
/// Includes essential information such as Sale ID, user ID, status and creation timestamp.
/// </remarks>
public class GetAllSalesResponse
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
    /// Gets or sets the current status of the Sale (e.g., Open, Confirmed).
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date and time the Sale was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}

