using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Handler responsible for processing <see cref="UpdateSaleCommand"/> requests.
/// </summary>
/// <remarks>
/// This handler relies on MediatR's pipeline behavior to automatically perform validation using
/// registered FluentValidation validators, so explicit validation calls in this handler are removed.
/// It retrieves the existing Sale from the repository, applies updates using <see cref="IMapper"/>.
/// persists the changes, and logs each step using <see cref="ILogger"/>. 
/// Returns <see cref="UpdateSaleResult"/> upon successful update or throws exceptions in case of errors.
/// </remarks>
public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateSaleHandler> _logger;

    // EventIds
    private static readonly EventId StartUpdateEvent = new(3301, nameof(StartUpdateEvent));
    private static readonly EventId SaleNotFoundEvent = new(3303, nameof(SaleNotFoundEvent));
    private static readonly EventId SaleUpdatedEvent = new(3304, nameof(SaleUpdatedEvent));
    private static readonly EventId UnexpectedErrorEvent = new(3399, nameof(UnexpectedErrorEvent));

    // LoggerMessage definitions
    private static readonly Action<ILogger, Guid, Exception?> LogStartUpdate =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            StartUpdateEvent,
            "Handling UpdateSaleCommand for ID: {SaleId}");

    private static readonly Action<ILogger, Guid, Exception?> LogSaleNotFound =
        LoggerMessage.Define<Guid>(
            LogLevel.Warning,
            SaleNotFoundEvent,
            "Sale with ID {SaleId} not found");

    private static readonly Action<ILogger, Guid, Exception?> LogSaleUpdated =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            SaleUpdatedEvent,
            "Sale with ID {SaleId} updated successfully");

    private static readonly Action<ILogger, Guid, Exception> LogUnexpectedError =
        LoggerMessage.Define<Guid>(
            LogLevel.Error,
            UnexpectedErrorEvent,
            "Unexpected error while updating Sale with ID {SaleId}");

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleHandler"/> class.
    /// </summary>
    /// <param name="repository">The Sale repository instance.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    /// <param name="logger">The logger instance.</param>
    public UpdateSaleHandler(ISaleRepository repository, IMapper mapper, ILogger<UpdateSaleHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="UpdateSaleCommand"/> request and returns the updated Sale.
    /// </summary>
    /// <param name="command">The command containing updated Sale information.</param>
    /// <param name="cancellationToken">Cancellation token for the async operation.</param>
    /// <returns>The updated Sale result as <see cref="UpdateSaleResult"/>.</returns>
    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        try
        {
            LogStartUpdate(_logger, command.Id, null);

            var Sale = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (Sale == null)
            {
                LogSaleNotFound(_logger, command.Id, null);
                throw new KeyNotFoundException($"Sale with ID {command.Id} was not found.");
            }

            _mapper.Map(command, Sale);

            /// <summary>
            /// Validates business rules such as quantity limits and discount logic.
            /// Throws DomainException if any rule is violated.
            /// </summary>
            Sale.EnsureBusinessRulesAreMet();

            var updated = await _repository.UpdateAsync(Sale, cancellationToken);

            LogSaleUpdated(_logger, updated.Id, null);

            return _mapper.Map<UpdateSaleResult>(updated);
        }
        catch (Exception ex)
        {
            LogUnexpectedError(_logger, command.Id, ex);
            throw;
        }
    }
}
