using Ambev.DeveloperEvaluation.Application.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

/// <summary>
/// Query to retrieve a paginated list of products.
/// </summary>
/// <remarks>
/// This command is used to retrieve products from the system with support for pagination.
/// It returns a <see cref="PaginatedList{GetAllProductsResult}"/> upon execution.
/// </remarks>
public class GetAllProductsCommand : IRequest<PaginatedList<GetAllProductsResult>>
{
    /// <summary>
    /// Gets or sets the current page number. Default is 1.
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Gets or sets the number of items per page. Default is 10.
    /// </summary>
    public int Size { get; set; } = 10;
}
