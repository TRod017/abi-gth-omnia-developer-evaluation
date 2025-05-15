using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Handler responsible for processing <see cref="UpdateCartCommand"/> requests.
/// </summary>
/// <remarks>
/// This handler validates the update command using <see cref="UpdateCartValidator"/>.
/// It retrieves the existing cart from the repository, applies updates using <see cref="IMapper"/>,
/// persists the changes, and logs each step using <see cref="ILogger"/>. 
/// Returns <see cref="UpdateCartResult"/> upon successful update or throws exceptions in case of errors.
/// </remarks>
public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, UpdateCartResult>
{
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateCartHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCartHandler"/> class.
    /// </summary>
    /// <param name="repository">The cart repository instance.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    /// <param name="logger">The logger instance.</param>
    public UpdateCartHandler(ICartRepository repository, IMapper mapper, ILogger<UpdateCartHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="UpdateCartCommand"/> request and returns the updated cart.
    /// </summary>
    /// <param name="command">The command containing updated cart information.</param>
    /// <param name="cancellationToken">Cancellation token for the async operation.</param>
    /// <returns>The updated cart result as <see cref="UpdateCartResult"/>.</returns>
    public async Task<UpdateCartResult> Handle(UpdateCartCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling UpdateCartCommand for Cart ID: {CartId}", command.Id);

        try
        {
            var validator = new UpdateCartValidator();
            var validation = await validator.ValidateAsync(command, cancellationToken);

            if (!validation.IsValid)
            {
                _logger.LogWarning("Validation failed for UpdateCartCommand. Errors: {@Errors}", validation.Errors);
                throw new ValidationException(validation.Errors);
            }

            var cart = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (cart == null)
            {
                _logger.LogWarning("Cart with ID {CartId} not found", command.Id);
                throw new KeyNotFoundException($"Cart with ID {command.Id} was not found.");
            }

            _mapper.Map(command, cart);
            var updated = await _repository.UpdateAsync(cart, cancellationToken);

            _logger.LogInformation("Cart with ID {CartId} updated successfully", updated.Id);

            return _mapper.Map<UpdateCartResult>(updated);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating cart with ID {CartId}", command.Id);
            throw;
        }
    }
}
