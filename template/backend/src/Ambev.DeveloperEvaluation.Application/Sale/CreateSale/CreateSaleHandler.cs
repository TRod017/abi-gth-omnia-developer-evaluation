using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler responsible for processing <see cref="CreateSaleCommand"/> requests.
/// </summary>
/// <remarks>
/// This handler relies on MediatR's pipeline behavior to automatically perform validation using
/// registered FluentValidation validators, so explicit validation calls in this handler are removed.
/// It retrieves the associated Sale using <see cref="ISaleRepository"/>, verifies business rules,
/// creates a corresponding <see cref="Sale"/> entity, persists it using <see cref="ISaleRepository"/>.
/// Logs each step of the operation using <see cref="ILogger"/>. Returns a <see cref="CreateSaleResult"/> upon success.
/// </remarks>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ICartRepository _cartRepo;
    private readonly ISaleRepository _saleRepo;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSaleHandler> _logger;

    private static readonly EventId HandlingEvent = new(2001, nameof(CreateSaleHandler));
    private static readonly EventId MappingEvent = new(2002, "Mapping");
    private static readonly EventId PersistenceEvent = new(2003, "Persistence");
    private static readonly EventId UnhandledExceptionEvent = new(2500, "UnhandledException");

    private static readonly Action<ILogger, Guid, Exception?> LogHandling =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            HandlingEvent,
            "Handling CreateSaleCommand for SaleId: {SaleId}");

    private static readonly Action<ILogger, object, Exception?> LogMapped =
        LoggerMessage.Define<object>(
            LogLevel.Debug,
            MappingEvent,
            "Mapped Sale to Sale entity: {@Sale}");

    private static readonly Action<ILogger, Guid, Exception?> LogCreated =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            PersistenceEvent,
            "Sale created with ID: {SaleId}");

    private static readonly Action<ILogger, Guid, Exception> LogUnhandled =
        LoggerMessage.Define<Guid>(
            LogLevel.Critical,
            UnhandledExceptionEvent,
            "Unhandled exception occurred while creating sale for SaleId: {SaleId}");

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleHandler"/> class.
    /// </summary>
    /// <param name="cartRepo">Repository to access Cart data.</param>
    /// <param name="saleRepo">Repository to persist sale data.</param>
    /// <param name="mapper">AutoMapper instance for object mapping.</param>
    /// <param name="logger">Logger instance for structured logging.</param>
    public CreateSaleHandler(
        ICartRepository cartRepo,
        ISaleRepository saleRepo,
        IMapper mapper,
        ILogger<CreateSaleHandler> logger)
    {
        _cartRepo = cartRepo;
        _saleRepo = saleRepo;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="CreateSaleCommand"/> request and returns the created sale result.
    /// </summary>
    /// <param name="request">The command containing the Sale reference and sale data.</param>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    /// <returns>The result of the created sale as <see cref="CreateSaleResult"/>.</returns>
    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        LogHandling(_logger, request.CartId, null);

        try
        {
            var cart = await _cartRepo.GetByIdAsync(request.CartId);
            if (cart is null)
            {
                throw new InvalidOperationException("Cart not found");
            }

            var sale = _mapper.Map<Sale>(cart);

            /// <summary>
            /// Validates business rules such as quantity limits and discount logic.
            /// Throws DomainException if any rule is violated.
            /// </summary>
            sale.EnsureBusinessRulesAreMet();

            LogMapped(_logger, sale, null);

            var created = await _saleRepo.CreateAsync(sale, cancellationToken);

            /// <summary>
            /// Logs the sale creation event including sale metadata such as total and creation date.
            /// This simulates the SaleCreated event.
            /// </summary>
            _logger.LogInformation("SaleCreated: UserId={UserId}, SaleId={SaleId}, Total={Total}, Date={Date}",
                sale.UserId, sale.Id, sale.TotalWithDiscount, sale.CreatedAt);

            LogCreated(_logger, created.Id, null);

            return _mapper.Map<CreateSaleResult>(created);
        }
        catch (Exception ex)
        {
            LogUnhandled(_logger, request.CartId, ex);
            throw;
        }
    }
}
