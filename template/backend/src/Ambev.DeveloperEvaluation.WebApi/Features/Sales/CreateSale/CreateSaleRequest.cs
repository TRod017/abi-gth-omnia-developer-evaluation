using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Represents the request payload used to create a new Sale via the API.
/// </summary>
/// <remarks>
/// Includes the user identifier and a collection of Sale items to be added.
/// </remarks>
public class CreateSaleRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the user who owns the Sale.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the status of the Sale 
    /// </summary>
    public string Status { get; set; } 


    /// <summary>
    /// Gets or sets the list of items to include in the Sale.
    /// </summary>
    public List<CreateSaleItemRequest> Items { get; set; } = new();
}

/// <summary>
/// Represents a single item in the Sale during creation.
/// </summary>
/// <remarks>
/// Contains the product ID, quantity, and unit price at the time of Sale creation.
/// </remarks>
public class CreateSaleItemRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product to be added to the Sale.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product at the time it is added to the Sale.
    /// </summary>
    public decimal UnitPrice { get; set; }
}

