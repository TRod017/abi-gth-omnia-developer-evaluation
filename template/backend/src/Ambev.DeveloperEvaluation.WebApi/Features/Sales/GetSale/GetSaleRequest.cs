namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Represents the request payload used to retrieve a Sale by its unique identifier.
/// </summary>
/// <remarks>
/// This request is typically sent via the API to retrieve full details of a Sale,
/// using the provided <see cref="Id"/> as the lookup key.
/// </remarks>
public class GetSaleRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the Sale to retrieve.
    /// </summary>
    public Guid Id { get; set; }
}
