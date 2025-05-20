namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Represents the response returned by the API after successfully creating a Sale.
/// </summary>
/// <remarks>
/// Contains the unique identifier of the newly created Sale, which can be used
/// for further operations such as retrieval or updates.
/// </remarks>
public class CreateSaleResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created Sale.
    /// </summary>
    public Guid Id { get; set; }
}

