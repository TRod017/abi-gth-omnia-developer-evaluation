namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;

/// <summary>
/// Represents the request payload used to cancel an existing Sale via the API.
/// </summary>
/// <remarks>
/// Includes the Sale ID, user ID and cancellation flag.
/// </remarks>
public class CancelSaleRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the Sale to cancel.
    /// </summary>
    public Guid Id { get; set; }
}
