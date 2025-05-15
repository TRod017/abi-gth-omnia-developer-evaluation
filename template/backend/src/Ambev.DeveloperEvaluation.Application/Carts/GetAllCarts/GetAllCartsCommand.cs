using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;

/// <summary>
/// Query to retrieve all carts.
/// </summary>
/// <remarks>
/// This command is used to retrieve a complete list of carts in the system.
/// It returns a collection of <see cref="GetAllCartsResult"/> upon execution.
/// </remarks>
public class GetAllCartsCommand : IRequest<IReadOnlyCollection<GetAllCartsResult>>
{
}
