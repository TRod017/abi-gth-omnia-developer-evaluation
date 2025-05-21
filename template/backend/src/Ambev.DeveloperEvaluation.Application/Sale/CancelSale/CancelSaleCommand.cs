using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Command for cancelling an existing Sale.
/// </summary>
/// <remarks>
/// This command is used to encapsulate the intent to cancel a Sale,
/// including the associated user and cancellation status. It returns a <see cref="CancelSaleResult"/> upon execution.
/// </remarks>
public class CancelSaleCommand : IRequest<CancelSaleResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the Sale to be cancelled.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the sale is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; } = true;
}
