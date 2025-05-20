using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale.SaleItem;

/// <summary>
/// Represents an item included in a sale creation request.
/// </summary>
/// <remarks>
/// Contains the necessary information about a product being sold,
/// such as ProductId, Quantity, and UnitPrice at the time of the sale.
/// </remarks>
public class CreateSaleItemCommand : IRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product being sold.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product at the time of sale.
    /// </summary>
    public decimal UnitPrice { get; set; }
}
