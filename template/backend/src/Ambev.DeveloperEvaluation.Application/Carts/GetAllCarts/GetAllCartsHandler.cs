using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;

/// <summary>
/// Handler responsible for processing <see cref="GetAllCartsCommand"/> requests.
/// </summary>
/// <remarks>
/// This handler retrieves all carts from the repository and maps them to the
/// <see cref="GetAllCartsResult"/> response model using <see cref="IMapper"/>. It also logs
/// the operation using <see cref="ILogger"/> for observability.
/// </remarks>
public class GetAllCartsHandler : IRequestHandler<GetAllCartsCommand, IReadOnlyCollection<GetAllCartsResult>>
{
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllCartsHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllCartsHandler"/> class.
    /// </summary>
    /// <param name="repository">The cart repository for data access.</param>
    /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
    /// <param name="logger">The logger instance for logging operations.</param>
    public GetAllCartsHandler(ICartRepository repository, IMapper mapper, ILogger<GetAllCartsHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="GetAllCartsCommand"/> request and returns a list of carts.
    /// </summary>
    /// <param name="command">The request command to retrieve all carts.</param>
    /// <param name="cancellationToken">Cancellation token for the async operation.</param>
    /// <returns>A collection of <see cref="GetAllCartsResult"/> representing the carts.</returns>
    public async Task<IReadOnlyCollection<GetAllCartsResult>> Handle(GetAllCartsCommand command, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Retrieving all carts from repository");

            var carts = await _repository.GetAllAsync(cancellationToken);

            _logger.LogInformation("Retrieved {Count} carts", carts.Count);

            return _mapper.Map<IReadOnlyCollection<GetAllCartsResult>>(carts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while retrieving all carts");
            throw;
        }
    }
}
