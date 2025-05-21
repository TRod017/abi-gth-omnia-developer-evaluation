namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;

/// <summary>
/// Represents the request payload used to delete a Sale by its unique identifier.
/// </summary>
/// <remarks>
/// This request is typically sent via the API to initiate the deletion of a Sale,
/// using the provided <see cref="Id"/> as reference.
/// </remarks>
public class DeleteSaleRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the Sale to be deleted.
    /// </summary>
    public Guid Id { get; set; }
}

