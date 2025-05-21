namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// API response model returned after successfully updating a Sale.
/// </summary>
/// <remarks>
/// Contains the unique identifier of the updated Sale, which can be used to confirm
/// the update operation or reference the updated resource in subsequent calls.
/// </remarks>
public class UpdateSaleResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the updated Sale.
    /// </summary>
    public Guid Id { get; set; }
}

