using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Handler for processing <see cref="UpdateCartCommand"/> requests.
/// </summary>
public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, UpdateCartResult>
{
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateCartHandler> _logger;

    public UpdateCartHandler(ICartRepository repository, IMapper mapper, ILogger<UpdateCartHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

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
