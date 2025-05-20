using Ambev.DeveloperEvaluation.Domain.Enums;

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
    /// Gets or sets the current status of the Sale.
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the Sale was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the Sale was last updated, if applicable.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}
