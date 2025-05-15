using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;

/// <summary>
/// Command for retrieving all carts.
/// </summary>
public class GetAllCartsCommand : IRequest<IReadOnlyCollection<GetAllCartsResult>>
{
}
