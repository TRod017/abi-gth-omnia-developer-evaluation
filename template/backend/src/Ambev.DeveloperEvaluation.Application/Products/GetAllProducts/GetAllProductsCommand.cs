using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

/// <summary>
/// Query to retrieve all available products.
/// </summary>
/// <remarks>
/// This command does not require any parameters and will return a collection of <see cref="GetAllProductsResult"/>.
/// </remarks>
public class GetAllProductsCommand : IRequest<IEnumerable<GetAllProductsResult>>
{
}
