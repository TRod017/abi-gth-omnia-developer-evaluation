using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Specifications.Cart;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.Domain.Specifications.Product.ConfirmSale;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Handler responsible for processing <see cref="CreateCartCommand"/> requests.
/// </summary>
/// <remarks>
/// This handler relies on MediatR's pipeline behavior to automatically perform validation using
/// registered FluentValidation validators, so explicit validation calls in this handler are removed.
/// It maps the command to a <see cref="Cart"/> entity using <see cref="IMapper"/>.
/// Persists it using <see cref="ICartRepository"/>, and logs each step of the operation
/// using <see cref="ILogger"/>. Returns a <see cref="CreateCartResult"/> upon successful creation.
/// </remarks>
public class CreateCartHandler : IRequestHandler<CreateCartCommand, CreateCartResult>
{
    private readonly ICartRepository _repository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCartHandler> _logger;

    private static readonly EventId HandlingEvent = new(1001, nameof(CreateCartHandler));
    private static readonly EventId MappingEvent = new(1002, "Mapping");
    private static readonly EventId PersistenceEvent = new(1003, "Persistence");
    private static readonly EventId UnhandledExceptionEvent = new(1500, "UnhandledException");

    private static readonly Action<ILogger, Guid, Exception?> LogHandling =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            HandlingEvent,
            "Handling CreateCartCommand for UserId: {UserId}");

    private static readonly Action<ILogger, object, Exception?> LogMapped =
        LoggerMessage.Define<object>(
            LogLevel.Debug,
            MappingEvent,
            "Mapped CreateCartCommand to Cart entity: {@Cart}");

    private static readonly Action<ILogger, Guid, Exception?> LogCreated =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            PersistenceEvent,
            "Cart created with ID: {CartId}");

    private static readonly Action<ILogger, Guid, Exception> LogUnhandled =
        LoggerMessage.Define<Guid>(
            LogLevel.Critical,
            UnhandledExceptionEvent,
            "Unhandled exception occurred while creating cart for UserId: {UserId}");

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCartHandler"/> class.
    /// </summary>
    /// <param name="repository">The cart repository for persistence operations.</param>
    /// <param name="mapper">The AutoMapper instance used for object mapping.</param>
    /// <param name="logger">The logger used to log operational steps.</param>
    public CreateCartHandler(ICartRepository repository, IProductRepository productRepository, IMapper mapper, ILogger<CreateCartHandler> logger)
    {
        _repository = repository;
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="CreateCartCommand"/> request and returns the created cart result.
    /// </summary>
    /// <param name="command">The command containing the cart data to be created.</param>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    /// <returns>The result of the created cart as <see cref="CreateCartResult"/>.</returns>
    public async Task<CreateCartResult> Handle(CreateCartCommand command, CancellationToken cancellationToken)
    {
        LogHandling(_logger, command.UserId, null);

        try
        {
            var cart = new Cart
            {
                Id = Guid.NewGuid(),
                UserId = command.UserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Items = new List<Ambev.DeveloperEvaluation.Domain.Entities.CartItem>()
            };

            foreach (var itemCommand in command.Items)
            {
                var product = await _productRepository.GetByIdAsync(itemCommand.ProductId);
                if (product is null)
                    throw new InvalidOperationException($"Product not found: {itemCommand.ProductId}");

                SpecificationValidator.Validate(
                    (product, itemCommand.Quantity),
                    (new ProductStockAvailableSpecification(), $"Insufficient stock for product {product.Name}")
                );

                var cartItem = new Ambev.DeveloperEvaluation.Domain.Entities.CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    UnitPrice = product.UnitPrice,
                    Quantity = itemCommand.Quantity,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                cart.Items.Add(cartItem);
            }

            LogMapped(_logger, cart, null);

            SpecificationValidator.Validate(cart,
                (new CartMustHaveItemsSpecification(), "Cart must contain at least one item"));

            /// <summary>
            /// Validates business rules such as quantity limits and discount logic.
            /// Throws DomainException if any rule is violated.
            /// </summary>
            cart.EnsureBusinessRulesAreMet();

            var created = await _repository.CreateAsync(cart, cancellationToken);

            /// <summary>
            /// Logs the sale creation event including cart metadata such as total and creation date.
            /// This simulates the SaleCreated event.
            /// </summary>
            _logger.LogInformation("SaleCreated: UserId={UserId}, CartId={CartId}, Total={Total}, Date={Date}",
                cart.UserId, cart.Id, cart.Total, cart.CreatedAt);

            LogCreated(_logger, created.Id, null);

            return _mapper.Map<CreateCartResult>(created);
        }
        catch (Exception ex)
        {
            LogUnhandled(_logger, command.UserId, ex);
            throw;
        }
    }
}
