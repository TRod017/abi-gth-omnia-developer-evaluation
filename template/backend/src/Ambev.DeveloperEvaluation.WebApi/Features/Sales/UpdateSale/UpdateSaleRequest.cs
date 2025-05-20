namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Represents the request payload used to update an existing Sale via the API.
/// </summary>
/// <remarks>
/// Includes the Sale ID, updated status, and a list of Sale items to update.
/// </remarks>
public class UpdateSaleRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the Sale to update.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user associated with the Sale.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the updated status of the Sale (e.g., Open, Confirmed).
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of Sale items to be updated.
    /// </summary>
    public List<UpdateSaleItemRequest> Items { get; set; } = new();
}


