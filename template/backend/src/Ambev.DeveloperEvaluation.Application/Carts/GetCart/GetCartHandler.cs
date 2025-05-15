using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Handler responsible for processing <see cref="GetCartCommand"/> requests.
/// </summary>
/// <remarks>
/// This handler validates the command, fetches the cart from the repository,
/// and maps it to a <see cref="GetCartResult"/>. It logs each step for observability.
/// </remarks>
public class GetCartHandler : IRequestHandler<GetCartCommand, GetCartResult>
{
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCartHandler> _logger;

    public GetCartHandler(ICartRepository repository, IMapper mapper, ILogger<GetCartHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<GetCartResult> Handle(GetCartCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetCartCommand for Cart ID: {CartId}", command.Id);

        try
        {
            var validator = new GetCartValidator();
            var validation = await validator.ValidateAsync(command, cancellationToken);

            if (!validation.IsValid)
            {
                _logger.LogWarning("Validation failed for GetCartCommand. Errors: {@Errors}", validation.Errors);
                throw new ValidationException(validation.Errors);
            }

            var cart = await _repository.GetByIdAsync(command.Id, cancellationToken);

            if (cart == null)
            {
                _logger.LogWarning("Cart with ID {CartId} not found", command.Id);
                throw new KeyNotFoundException($"Cart with ID {command.Id} was not found.");
            }

            _logger.LogInformation("Cart with ID {CartId} retrieved successfully", command.Id);

            return _mapper.Map<GetCartResult>(cart);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred while handling GetCartCommand for ID: {CartId}", command.Id);
            throw;
        }
    }
}
