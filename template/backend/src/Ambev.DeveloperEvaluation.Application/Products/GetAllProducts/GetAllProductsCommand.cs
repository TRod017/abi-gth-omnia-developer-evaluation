using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

/// <summary>
/// Query to retrieve all products.
/// </summary>
/// <remarks>
/// This command is used to retrieve a complete list of products in the system. 
/// It returns a collection of <see cref="GetAllProductsResult"/> upon execution.
/// </remarks>
public class GetAllProductsCommand : IRequest<IReadOnlyCollection<GetAllProductsResult>>
{
}
