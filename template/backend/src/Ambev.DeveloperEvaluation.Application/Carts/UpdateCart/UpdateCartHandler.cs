using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Specifications.Product.ConfirmSale;
using Ambev.DeveloperEvaluation.Domain.Specifications;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Handler responsible for processing <see cref="UpdateCartCommand"/> requests.
/// </summary>
/// <remarks>
/// This handler relies on MediatR's pipeline behavior to automatically perform validation using
/// registered FluentValidation validators, so explicit validation calls in this handler are removed.
/// It retrieves the existing cart from the repository, applies updates using <see cref="IMapper"/>.
/// persists the changes, and logs each step using <see cref="ILogger"/>. 
/// Returns <see cref="UpdateCartResult"/> upon successful update or throws exceptions in case of errors.
/// </remarks>
public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, UpdateCartResult>
{
    private readonly ICartRepository _repository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateCartHandler> _logger;

    // EventIds
    private static readonly EventId StartUpdateEvent = new(3301, nameof(StartUpdateEvent));
    private static readonly EventId CartNotFoundEvent = new(3303, nameof(CartNotFoundEvent));
    private static readonly EventId CartUpdatedEvent = new(3304, nameof(CartUpdatedEvent));
    private static readonly EventId UnexpectedErrorEvent = new(3399, nameof(UnexpectedErrorEvent));

    // LoggerMessage definitions
    private static readonly Action<ILogger, Guid, Exception?> LogStartUpdate =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            StartUpdateEvent,
            "Handling UpdateCartCommand for ID: {CartId}");

    private static readonly Action<ILogger, Guid, Exception?> LogCartNotFound =
        LoggerMessage.Define<Guid>(
            LogLevel.Warning,
            CartNotFoundEvent,
            "Cart with ID {CartId} not found");

    private static readonly Action<ILogger, Guid, Exception?> LogCartUpdated =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            CartUpdatedEvent,
            "Cart with ID {CartId} updated successfully");

    private static readonly Action<ILogger, Guid, Exception> LogUnexpectedError =
        LoggerMessage.Define<Guid>(
            LogLevel.Error,
            UnexpectedErrorEvent,
            "Unexpected error while updating cart with ID {CartId}");

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCartHandler"/> class.
    /// </summary>
    /// <param name="repository">The cart repository instance.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    /// <param name="logger">The logger instance.</param>
    public UpdateCartHandler(ICartRepository repository, IProductRepository productRepository, IMapper mapper, ILogger<UpdateCartHandler> logger)
    {
        _repository = repository;
        _productRepository = productRepository;
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
        try
        {
            LogStartUpdate(_logger, command.Id, null);

            var cart = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (cart == null)
            {
                LogCartNotFound(_logger, command.Id, null);
                throw new KeyNotFoundException($"Cart with ID {command.Id} was not found.");
            }

            _mapper.Map(command, cart);

            // For each item in the cart, fetch the corresponding product from the repository
            // and validate that there is sufficient stock available using the ProductStockAvailableSpecification.
            // Throws an exception if the product is not found or if stock is insufficient.
            foreach (var item in cart.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null)
                    throw new InvalidOperationException($"Product not found: {item.ProductId}");

                SpecificationValidator.Validate(
                    (product, item.Quantity),
                    (new ProductStockAvailableSpecification(), $"Insufficient stock for product {product.Name}")
                );

            }

            /// <summary>
            /// Validates business rules such as quantity limits and discount logic.
            /// Throws DomainException if any rule is violated.
            /// </summary>
            cart.EnsureBusinessRulesAreMet();

            var updated = await _repository.UpdateAsync(cart, cancellationToken);

            LogCartUpdated(_logger, updated.Id, null);

            return _mapper.Map<UpdateCartResult>(updated);
        }
        catch (Exception ex)
        {
            LogUnexpectedError(_logger, command.Id, ex);
            throw;
        }
    }
}
