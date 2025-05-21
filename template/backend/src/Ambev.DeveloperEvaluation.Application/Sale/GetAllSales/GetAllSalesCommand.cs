using MediatR;
using Ambev.DeveloperEvaluation.Application.Common;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

/// <summary>
/// Query to retrieve a paginated list of Sales.
/// </summary>
/// <remarks>
/// This command is used to retrieve a specific page of Sales in the system,
/// including pagination parameters such as page number and page size.
/// It returns a <see cref="PaginatedList{GetAllSalesResult}"/> upon execution.
/// </remarks>
public class GetAllSalesCommand : IRequest<PaginatedList<GetAllSalesResult>>
{
    /// <summary>
    /// Gets or sets the number of the page to retrieve (1-based).
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Gets or sets the number of items per page.
    /// </summary>
    public int Size { get; set; } = 10;
}
