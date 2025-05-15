using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;

/// <summary>
/// Handler responsible for processing <see cref="GetAllCartsCommand"/> requests.
/// </summary>
public class GetAllCartsHandler : IRequestHandler<GetAllCartsCommand, IReadOnlyCollection<GetAllCartsResult>>
{
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllCartsHandler> _logger;

    public GetAllCartsHandler(ICartRepository repository, IMapper mapper, ILogger<GetAllCartsHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

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
