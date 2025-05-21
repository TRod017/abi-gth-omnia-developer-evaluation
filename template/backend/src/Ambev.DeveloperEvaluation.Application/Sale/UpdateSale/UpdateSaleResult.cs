namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Represents the response model returned after successfully updating a Sale.
/// </summary>
/// <remarks>
/// This DTO contains the unique identifier of the updated Sale, typically used
/// to confirm the update operation and reference the Sale in subsequent actions.
/// </remarks>
public class UpdateSaleResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the updated Sale.
    /// </summary>
    public Guid Id { get; set; }
}
